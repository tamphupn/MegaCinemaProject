using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaService;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class StatusController : BaseController
    {
        // GET: AdminDashboard/Status
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusViewModel statusVm)
        {
            if (ModelState.IsValid)
            {
                //
            }
            return View(statusVm);
        }

    }
}