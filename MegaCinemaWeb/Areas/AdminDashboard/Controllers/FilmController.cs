using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmController : Controller
    {
        IFilmService _filmService;
        IFilmRatingService _filmRatingService;
        IStatusService _statusService;
        public FilmController(IFilmService filmService, IStatusService statusService, IFilmRatingService filmRatingService)
        {
            _filmService = filmService;
            _statusService = statusService;
            _filmRatingService = filmRatingService;
        }

        // GET: AdminDashboard/Film
        public ActionResult Index()
        {
            var result = Mapper.Map<IEnumerable<FilmViewModel>>(_filmService.GetAll());
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FilmStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            ViewBag.FilmRatingID = new SelectList(_filmRatingService.GetAll(), "RatingID", "RatingName");
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

    }
}