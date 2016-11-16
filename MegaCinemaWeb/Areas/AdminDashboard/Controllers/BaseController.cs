using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class BaseController : Controller
    {
        // GET: AdminDashboard/Base
        public ActionResult Index()
        {
            return View();
        }
    }
}