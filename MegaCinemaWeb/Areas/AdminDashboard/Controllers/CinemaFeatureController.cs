using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaWeb.Infrastructure.Extensions;


namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class CinemaFeatureController : BaseController
    {
        ICinemaFeatureService _cinemaFeatureService;
        public CinemaFeatureController(ICinemaFeatureService cinemaFeatureService)
        {
            _cinemaFeatureService = cinemaFeatureService;
        }

        // GET: AdminDashboard/CinemaFeature
        public ActionResult Index()
        {
            IEnumerable<CinemaFeature> cinemaFeatures = _cinemaFeatureService.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CinemaFeatureViewModel cinemaFeatureViewModel)
        {
            if(ModelState.IsValid)
            {
                cinemaFeatureViewModel.CreatedDate = DateTime.Now;
                CinemaFeature cinemaFeature = new CinemaFeature();
                cinemaFeature.UpdateCinemaFeature(cinemaFeatureViewModel);
                if(cinemaFeature != null)
                {
                    _cinemaFeatureService.Add(cinemaFeature);
                    _cinemaFeatureService.SaveChanges();
                    
                    return RedirectToAction(("Index"));
                }
            }

            return RedirectToAction("Create");
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            CinemaFeature cineamFeature = _cinemaFeatureService.Find(id);
            if(cineamFeature != null)
            {
                if(_cinemaFeatureService.Delete(cineamFeature) != null) {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}