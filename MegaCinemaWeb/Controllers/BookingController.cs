using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using MegaCinemaCommon.BookingTicket;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace MegaCinemaWeb.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private IFilmCalendarCreateService _filmCalendarCreateService;
        private ITimeSessionService _timeSessionService;
        private IFilmService _filmService;
        private IStaffService _staffService;
        private ICustomerService _customerService;
        private IBookingTicketService _bookingTicketService;
        public BookingController(IFilmCalendarCreateService filmCalendarCreateService, ITimeSessionService timeSessionService, IFilmService filmService, IStaffService staffService, ICustomerService customerService,IBookingTicketService bookingTicketService)
        {
            _filmCalendarCreateService = filmCalendarCreateService;
            _timeSessionService = timeSessionService;
            _filmService = filmService;
            _customerService = customerService;
            _staffService = staffService;
            _bookingTicketService = bookingTicketService;
        }
        // GET: Booking
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult FilmSessionDetail(int id)
        {
            //Get thông tin phòng chiếu bộ phim ở tất cả các rạp trong hệ thống
            var result1 = _staffService.Find(3);
            var result = _filmCalendarCreateService.FilmCalendarOfFilm(id);
            var resultVm = Mapper.Map<IEnumerable<FilmCalendarCreateViewModel>>(result);

            var filmDetail = _filmService.Find(id);
            ViewData["FilmDetailSession"] = Mapper.Map<FilmViewModel>(filmDetail);
            Session["FilmDetailSendTicket"] = filmDetail;
            return View(resultVm);
        }

        [HttpPost]
        public ActionResult BookingSession(int sessionFilm, int? adultTicket, int? adultVip)
        {
            //Load thông tin vé trong hệ thống
            TicketDetailViewModel model = new TicketDetailViewModel();

            model.Adult = adultTicket == null ? 0 : adultTicket;
            model.AdultVip = adultVip == null ? 0 : adultVip;

            if (model.Adult + model.AdultVip > ParametersContrants.NUMBER_TICKET)
            {
                return Json(new
                {
                    redirectUrl = "none",
                    alertMessage = "Số vé một lần đặt không được quá 5 vé cho cả 2 loại",
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }

            TempData["TicketDetail"] = model;
            return Json(new
            {
                redirectUrl = Url.Action("TicketBookingView", new { sessionFilm = sessionFilm }),
                alertMessage = "none",
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TicketBookingView(int sessionFilm)
        {
            //Lấy thông tin phòng vé hiện tại
            var result = _timeSessionService.Find(sessionFilm);
            var resultVm = Mapper.Map<TimeSessionViewModel>(result);
            if (resultVm == null) return new HttpStatusCodeResult(404);

            //trả về ID session phim và object phòng vé
            Session["timeDetail"] = result;
            Session["userIDPhim"] = sessionFilm;
            ViewData["detailSessionTicket"] = TempData["TicketDetail"];
            ViewData["InfoTimeSession"] = sessionFilm;
            return View(resultVm);
        }

        public ContentResult StateSeatRoom(int sessionFilm)
        {
            var result = _timeSessionService.Find(sessionFilm);
            var resultVm = Mapper.Map<TimeSessionViewModel>(result);
            return Content(JsonConvert.SerializeObject(resultVm), "application/json");
        }

        public PartialViewResult UpdateSeatState()
        {
            int id = (int)Session["userIDPhim"];
            var result = _timeSessionService.Find(id);
            var resultVm = Mapper.Map<TimeSessionViewModel>(result);
            ViewData["FilmSessionState"] = id;
            return PartialView("_Update", resultVm);
        }

        [HttpPost]
        public JsonResult UpdateSeatUserTake(int? xLocation, int? yLocation, int? sessionFilm, string ticketValue)
        {
            //Check null
            if (xLocation == null || yLocation == null || sessionFilm == null)
            {
                return Json(new
                {
                    status = "KO"
                }, JsonRequestBehavior.AllowGet);
            }

            //Lấy thông tin time session của phim
            var result = _timeSessionService.Find((int)sessionFilm);

            //serial ticket list - Mã hóa state ghế hiện tại theo các ghế được đặt
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<TicketSubmitDetail> lstTicket = serial.Deserialize<List<TicketSubmitDetail>>(ticketValue);

            //Mã hóa về seat state
            SeatState sumary = BookingTimeHelpers.ConvertJsontoBookingTime(result.SeatTableState);
            foreach (var item in lstTicket)
            {
                sumary = BookingTimeHelpers.ChangeStateSeat(Convert.ToInt32(item.xLocation),
                    Convert.ToInt32(item.yLocation), 0, sumary);
            }
            //lấy customer ID
            var customerId = _customerService.FindCustomerID(User.Identity.GetUserId());
            if (customerId == 0)
            {
                return Json(new
                {
                    status = "KO",
                }, JsonRequestBehavior.AllowGet);
            }

            //lấy film detail
            var filmDetail = (Film)Session["FilmDetailSendTicket"];
            
            //insert dữ liệu vào database
            var resultState = _bookingTicketService.AddTicketToDB(filmDetail, customerId, result.TimeSessionID, sumary);

            //kiểm tra kết quả insert
            if (resultState)
            {
                SeatRoomStateFilmHub.UpdateSeatState();
                return Json(new
                {
                    status = "OK",
                    x = xLocation,
                    y = yLocation,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = "KO",
                }, JsonRequestBehavior.AllowGet);

            }
            //var filmDetail = (Film)Session["FilmDetailSendTicket"];

            ////detail prepare: FilmID, RoomID, TimeDetail, Prices,Payments Day, CustomerID, statusID, seattable state


            //_timeSessionService.SaveChanges();

            

            
        }

    }
}