using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmCategoryController : Controller
    {
        // GET: AdminDashboard/FilmCategory
        public ActionResult Index()
        {
            return Content("hjkhgjghjghj");
        }

        [HttpGet]
        public ActionResult Create()
        {
            //load drop down list
            return View();
        }
    }
}