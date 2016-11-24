using MegaCinemaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class PromotionCineController : BaseController
    {
        IPromotionCineService _promotionCineService;
        IStatusService _statusService;
        ICinemaService _cinemaService;
        IPromotionService _promotionService;

        public PromotionCineController(IPromotionCineService promotionCineService, IStatusService statusService, ICinemaService cinemaService, IPromotionService promotionService)
        {
            _promotionCineService = promotionCineService;
            _statusService = statusService;
            _cinemaService = cinemaService;
            _promotionService = promotionService;
        }

        // GET: AdminDashboard/PromotionCine
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CinemaID = new SelectList(_promotionCineService.GetAllCinemaFullName(), "CinemaID", "CinemaFullName");
            ViewBag.PromotionID = new SelectList(_promotionCineService.GetAllPromotionHeader(), "PromotionID", "PromotionHeader");
            ViewBag.PromotionCineStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");

            return View();
        }
    }
}