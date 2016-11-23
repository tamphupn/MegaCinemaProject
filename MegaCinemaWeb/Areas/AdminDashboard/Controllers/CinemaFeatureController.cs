using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class CinemaFeatureController : Controller
    {
        // GET: AdminDashboard/CinemaFeature
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
        public ActionResult Create(CinemaFeatureViewModel viewModel)
        {
            Console.WriteLine(viewModel.FeatureDescription);
            if(ModelState.IsValid)
            {
                Console.WriteLine("aldjfljasdfdjsf");
            }
            return RedirectToAction(("Index"));
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return RedirectToAction("Index");
        }
    }
}