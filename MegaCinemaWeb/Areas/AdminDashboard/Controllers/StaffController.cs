using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class StaffController : BaseController
    {
        // GET: AdminDashboard/Staff
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
    }
}