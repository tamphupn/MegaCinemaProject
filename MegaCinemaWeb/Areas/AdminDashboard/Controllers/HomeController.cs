using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class HomeController : Controller
    {
        // GET: AdminDashboard/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}