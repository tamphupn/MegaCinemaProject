using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmFeatureController : Controller
    {
        // GET: AdminDashboard/FilmFeature
        public ActionResult Index()
        {
            return View();
        }
    }
}