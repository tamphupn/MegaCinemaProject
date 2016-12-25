using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaService;
using AutoMapper;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Controllers
{
    public class HomeController : Controller
    {
        IStatusService _statusService;
        IFilmService _filmService;
        public HomeController(IStatusService statusService, IFilmService filmService)
        {
            _statusService = statusService;
            _filmService = filmService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var result = _statusService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<StatusViewModel>>(result);
            return View(resultVm);
        }

        public ActionResult HomePage()
        {
            var filmSlider = _filmService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<FilmViewModel>>(filmSlider);
            return View(resultVm);
        }
    }
}