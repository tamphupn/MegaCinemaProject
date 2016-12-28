using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MegaCinemaModel.Models;
using MegaCinemaWeb.Models;
using MegaCinemaService;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class RoomFilmController : BaseController
    {
        private ICinemaService _cinemaService;
        private IStaffService _staffService;
        private IRoomFilmService _roomFilmService;

        public RoomFilmController(ICinemaService cinemaService, IStaffService staffService,
            IRoomFilmService roomFilmService)
        {
            _cinemaService = cinemaService;
            _staffService = staffService;
            _roomFilmService = roomFilmService;
        }

        // GET: AdminDashboard/RoomFilm
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRoomDetail()
        {
            //Load danh sách các rạp chiếu phim 
            var result = _cinemaService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaViewModel>>(result);
            ViewBag.CinemaID = new SelectList(resultVm, "CinemaID", "CinemaFullName");

            //Load trạng thái của một phòng chiếu
            var listStatus = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Đang hoạt động", Value = "AC"},
                    new SelectListItem {Text = "Không hoạt động", Value = "NOT"},
                };

            ViewBag.CinemaStatusID = new SelectList(listStatus, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoomDetail(RoomFilmDetailViewModel roomFilmDetailVm)
        {
            if (ModelState.IsValid)
            {
                TempData["RoomFilmTable"] = roomFilmDetailVm;
                return RedirectToAction("SeatTableDetail", "RoomFilm");
            }
            //Load danh sách các rạp chiếu phim 
            var result = _cinemaService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaViewModel>>(result);
            ViewBag.CinemaID = new SelectList(resultVm, "CinemaID", "CinemaFullName");

            //Load trạng thái của một phòng chiếu
            var listStatus = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Đang hoạt động", Value = "AC"},
                    new SelectListItem {Text = "Không hoạt động", Value = "NOT"},
                };
            ViewBag.CinemaStatusID = new SelectList(listStatus, "Value", "Text");

            return View(roomFilmDetailVm);
        }

        [HttpGet]
        public ActionResult SeatTableDetail()
        {
            var result = TempData["RoomFilmTable"];
            if (result != null)
            {
                return View(result);
            }
            return Content("error");
        }

    }
}