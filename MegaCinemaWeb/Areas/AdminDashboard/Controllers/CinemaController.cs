using MegaCinemaService;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class CinemaController : BaseController
    {
        //TEST
        ICinemaService _cinemaService;
        IStatusService _statusService;

        public CinemaController(ICinemaService cinemaService, IStatusService statusService)
        {
            _cinemaService = cinemaService;
            _statusService = statusService;
        }
        // GET: AdminDashboard/Cinema
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CinemaStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CinemaViewModel cinema)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            //Action/Controller
            return RedirectToAction("Index", "FoodList");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

      
        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }
    }
}