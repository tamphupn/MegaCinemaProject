using MegaCinemaService;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaWeb.Infrastructure.Extensions;
using MegaCinemaModel.Models;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class CinemaController : BaseController
    {
        //TEST
        ICinemaService _cinemaService;
        IStatusService _statusService;
        IStaffService _staffService;
        
        public CinemaController(ICinemaService cinemaService, IStatusService statusService, IStaffService staffService)
        {
            _cinemaService = cinemaService;
            _statusService = statusService;
            _staffService = staffService;
        }
        // GET: AdminDashboard/Cinema
        public ActionResult Index()
        {   //Show List Cinema in Database
            IEnumerable<Cinema> cinemas = _cinemaService.GetAll();
            return View(cinemas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CinemaStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            ViewBag.StaffId = new SelectList(_staffService.GetAll(), "StaffID", "StaffCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CinemaViewModel cinemaViewModel)
        {
            if (ModelState.IsValid)
            {
                Cinema cinema = new Cinema();
                cinemaViewModel.CreatedDate = DateTime.Now;
                cinema.UpdateCinema(cinemaViewModel);

                if(cinema != null)
                {
                    _cinemaService.Add(cinema);
                    _cinemaService.SaveChanges();
                    //Redirect To Index Action
                    return RedirectToAction("Index");
                }
            }
            //Action/Controller
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

      
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
    }
}