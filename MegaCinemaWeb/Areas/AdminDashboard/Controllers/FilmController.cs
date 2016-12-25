using AutoMapper;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Infrastructure.Extensions;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaCommon.DataCrawler;
using MegaCinemaCommon.ANNModel;
using MegaCinemaWeb.Infrastructure.Core;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmController : Controller
    {
        IFilmService _filmService;
        IStatusService _statusService;
        public FilmController(IFilmService filmService, IStatusService statusService)
        {
            _filmService = filmService;
            _statusService = statusService;
        }

        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _filmService.GetFilmListPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<FilmViewModel>()
            {
                Items = resultVm,
                MaxPage = CommonConstrants.PAGE_SIZE,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
                Count = resultVm.Count(),
            };

            return View(paginationSet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FilmStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmViewModel newFilm, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                //More information
                newFilm.CreatedDate = DateTime.Now;
                //newFilm.CreatedBy =
                newFilm.FilmPrefix = CommonConstrants.FILM_PREFIX;

                //thêm vào database và lưu kết quả
                Film result = new Film();
                result.UpdateFilm(newFilm);

                var resultFoodList = _filmService.Add(result);
                _filmService.SaveChanges();

                //if (newFilm == null)
                //    return RedirectToAction("Index", "Film");
                //else
                //{
                //    //fileUpload.SaveAs(pathImage);
                //    return RedirectToAction("Index", "Film");
                //}
            }
            return View();
        }

        public ActionResult PreditionIndex()
        {
            return View();
        }
        private IMDBViewModel MappingDataMovie(IMDBModel model)
        {
            IMDBViewModel modelresult = new IMDBViewModel();
            modelresult.MovieName = model.MovieName;
            modelresult.MovieBudget = model.MovieBudget;
            modelresult.MovieDuration = model.MovieDuration;
            modelresult.MovieReleaseDate = model.MovieReleaseDate;
            modelresult.MovieGenre = model.MovieGenre[0];
            modelresult.MovieDirector = model.MovieDirector[0];
            modelresult.MovieLink = model.MovieLink;
            modelresult.MovieLinkPoster = model.MovieLinkPoster;
            modelresult.MovieLinkTrailer = model.MovieLinkTrailer;
            modelresult.MovieProduction = model.MovieProduction[0];
            modelresult.MovieMusicby = model.MovieMusicby[0];
            modelresult.MovieStarActor = model.MovieStarActor[0];
            modelresult.MovieWriter = model.MovieWriter[0];

            for (int i = 1; i < model.MovieGenre.Count; i++)
            {
                modelresult.MovieGenre += ", " + model.MovieGenre[i];
            }

            for (int i = 1; i < model.MovieDirector.Count; i++)
            {
                modelresult.MovieDirector += ", " + model.MovieDirector[i];
            }

            for (int i = 1; i < model.MovieProduction.Count; i++)
            {
                modelresult.MovieProduction += ", " + model.MovieProduction[i];
            }

            for (int i = 1; i < model.MovieMusicby.Count; i++)
            {
                modelresult.MovieMusicby += ", " + model.MovieMusicby[i];
            }

            for (int i = 1; i < model.MovieStarActor.Count; i++)
            {
                modelresult.MovieStarActor += ", " + model.MovieStarActor[i];
            }

            for (int i = 1; i < model.MovieWriter.Count; i++)
            {
                modelresult.MovieWriter += ", " + model.MovieWriter[i];
            }

            return modelresult;
        }
        [HttpPost]
        public ActionResult GetDetailFilmFromIMDB(string filmName)
        {
            if (filmName == null) return Json(new
            {
                result = "KO",
                value = "null",
            }, JsonRequestBehavior.AllowGet);

            //get data and serilize 
            IMDBModel result = IMDBModelHelper.GetMovieInformation(filmName);
            TempData["FilmUpdate"] = result;
            if (result == null) return Json(new
            {
                result = "KO",
                value = "null",
            }, JsonRequestBehavior.AllowGet);

            IMDBViewModel resultVm = new IMDBViewModel();            
          
            resultVm = MappingDataMovie(result);
            return Json(new
            {
                result = "OK",
                value = resultVm,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FilmPredition(string filmName)
        {
            var resultFilm = (IMDBModel)TempData["FilmUpdate"];
            if (resultFilm == null || String.IsNullOrEmpty(filmName))
                return Json(new
                {
                    result = "KO",
                    value = "null",
                }, JsonRequestBehavior.AllowGet);

            //Predition area 
            IMDBModelHelper.FilmState team = IMDBModelHelper.StartTrain(resultFilm);

            if (team == IMDBModelHelper.FilmState.DEFAULT)
                return Json(new
                {
                    result = "KO",
                    value = "null",
                }, JsonRequestBehavior.AllowGet);

            //Train data
            return Json(new
            {
                result = "OK",
                value = team,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}