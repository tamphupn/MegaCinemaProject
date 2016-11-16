using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class PromotionController : Controller
    {
        // GET: AdminDashboard/Promotion
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