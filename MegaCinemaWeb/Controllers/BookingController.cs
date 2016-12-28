using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using MegaCinemaCommon.BookingTicket;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaService;
using MegaCinemaWeb.Models;
using Newtonsoft.Json;

namespace MegaCinemaWeb.Controllers
{
    public class BookingController : Controller
    {
        private IFilmCalendarCreateService _filmCalendarCreateService;
        private ITimeSessionService _timeSessionService;
        private IFilmService _filmService;
        private IStaffService _staffService;
        public BookingController(IFilmCalendarCreateService filmCalendarCreateService, ITimeSessionService timeSessionService, IFilmService filmService, IStaffService staffService)
        {
            _filmCalendarCreateService = filmCalendarCreateService;
            _timeSessionService = timeSessionService;
            _filmService = filmService;
            _staffService = staffService;
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
            ViewData["FilmDetailSession"] = Mapper.Map<FilmViewModel>(_filmService.Find(id));
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
            if (xLocation == null || yLocation == null || sessionFilm == null)
            {
                return Json(new
                {
                    status = "KO"
                }, JsonRequestBehavior.AllowGet);
            }

            var result = _timeSessionService.Find((int)sessionFilm);

            //serial ticket list 
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<TicketSubmitDetail> lstTicket = serial.Deserialize<List<TicketSubmitDetail>>(ticketValue);

            SeatState sumary = BookingTimeHelpers.ConvertJsontoBookingTime(result.SeatTableState);
            foreach (var item in lstTicket)
            {
                sumary = BookingTimeHelpers.ChangeStateSeat(Convert.ToInt32(item.xLocation),
                    Convert.ToInt32(item.yLocation), 0, sumary);
            }
            result.SeatTableState = BookingTimeHelpers.ConvertBookingSessionToJson(sumary);
            _timeSessionService.SaveChanges();

            SeatRoomStateFilmHub.UpdateSeatState();

            return Json(new
            {
                status = "OK",
                x = xLocation,
                y = yLocation,
            }, JsonRequestBehavior.AllowGet);
        }

    }
}