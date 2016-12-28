using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaService;
using AutoMapper;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaWeb.Models;
using System;

namespace MegaCinemaWeb.Controllers
{
    public class HomeController : Controller
    {
        IStatusService _statusService;
        IFilmService _filmService;
        private IAdsBannerService _adsBannerService;
        private IEventTopicService _eventTopicService;
        private ICinemaService _cinemaService;
        private IPromotionService _promotionService;
        public HomeController(IStatusService statusService, IFilmService filmService, IAdsBannerService adsBannerService, IEventTopicService eventTopicService, ICinemaService cinemaService, IPromotionService promotionService, IPromotionCineService promotionCineService)
        {
            _statusService = statusService;
            _filmService = filmService;
            _adsBannerService = adsBannerService;
            _eventTopicService = eventTopicService;
            _cinemaService = cinemaService;
            _promotionService = promotionService;
            _promotionCineService = promotionCineService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var result = _statusService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<StatusViewModel>>(result);
            return View(resultVm);
        }


        #region Minh: site Home page
        public ActionResult HomePage()
        {
            //get Film banner site + banner + event topic 
            var filmSlider = _filmService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<FilmViewModel>>(filmSlider);

            var filmBanner = _adsBannerService.GetAll();
            var resultBannerVm = Mapper.Map<IEnumerable<AdsBannerViewModel>>(filmBanner);
            ViewData["AdsBanner"] = resultBannerVm;

            //get list theater + dropdownlist

            //get list film pending, plan, and special 
            var filmDangChieu = resultVm.Where(n => n.FilmStatus == "PEN");
            ViewData["FilmDangChieu"] = (filmDangChieu.Count() > 5)
                ? (filmDangChieu.Reverse().Take(5).Reverse())
                : (filmDangChieu);

            var filmSapChieu = resultVm.Where(n => n.FilmStatus == "REL");
            ViewData["FilmSapChieu"] = (filmSapChieu.Count() > 5)
                ? (filmSapChieu.Reverse().Take(5).Reverse())
                : (filmSapChieu);

            var filmSuatChieuDacBiet = resultVm.Where(n => n.FilmStatus == "REW");
            ViewData["FilmSuatChieuDacBiet"] = (filmSuatChieuDacBiet.Count() > 5)
                ? (filmSuatChieuDacBiet.Reverse().Take(5).Reverse())
                : (filmSuatChieuDacBiet);

            //get list events ??? chưa có 
            var eventTopic = _eventTopicService.GetAll();
            var eventTopicVm = Mapper.Map<IEnumerable<EventTopicViewModel>>(eventTopic);
            ViewData["EventTopic"] = eventTopicVm;

            //get list promotion 
            var promotion = _promotionService.GetAll(ParametersContrants.CONTENT_GET);
            var promotionVm = Mapper.Map<IEnumerable<PromotionViewModel>>(promotion);
            ViewData["PromotionBanner"] = promotionVm;
            return View(resultVm);
        }

        #endregion

        #region Nghĩa: site Phim

        public ActionResult PagePhim()
        {
            var filmSlider = _filmService.GetAll();
            var resultVm = Mapper.Map<IEnumerable<FilmViewModel>>(filmSlider);

            //get list film pending, plan, and special 
            var filmDangChieu = resultVm.Where(n => n.FilmStatus == "PEN").Select(n => n);
            int count = filmDangChieu.ToList().Count;


            ViewData["FilmDangChieu"] = (filmDangChieu.Count() > 5)
                ? (filmDangChieu.Reverse().Take(5).Reverse())
                : (filmDangChieu);

            var filmSapChieu = resultVm.Where(n => n.FilmStatus == "REL");
            ViewData["FilmSapChieu"] = (filmSapChieu.Count() > 5)
                ? (filmSapChieu.Reverse().Take(5).Reverse())
                : (filmSapChieu);

            var filmSuatChieuDacBiet = resultVm.Where(n => n.FilmStatus == "REW");
            ViewData["FilmSuatChieuDacBiet"] = (filmSuatChieuDacBiet.Count() > 5)
                ? (filmSuatChieuDacBiet.Reverse().Take(5).Reverse())
                : (filmSuatChieuDacBiet);

            return View();
        }

        public ActionResult Introduce()
        {
            return View();
        }


        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Hire()
        {
            return View();
        }

        public ActionResult FrequentlyAskedQuestions()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        #endregion

        #region Phúc: site Rạp&giá vé + ưu đãi
        public ActionResult PageRapGiaVe()
        {
            //get rạp chiếu phim mới nhất 
            var result = _cinemaService.FindLast();
            var resultVm = Mapper.Map<MegaCinemaModel.Models.Cinema, CinemaViewModel>(result);
            ViewData["LastCinema"] = resultVm;

            var promotion = _promotionService.GetAll(ParametersContrants.CONTENT_GET);
            var promotionVm = Mapper.Map<IEnumerable<PromotionViewModel>>(promotion);
            ViewData["ListTopPromotion"] = promotionVm;

            return View(resultVm);
        }
        
        private IPromotionCineService _promotionCineService;
        public ActionResult PageUuDai()
        {
            var promotion = _promotionService.GetAll(ParametersContrants.CONTENT_GET);
            var promotionVm = Mapper.Map<IEnumerable<PromotionViewModel>>(promotion);
            ViewData["PromotionBanner"] = promotionVm;
            
            var promotionCine = _promotionCineService.GetAll();
            var promotionCineVm = Mapper.Map<IEnumerable<PromotionCineViewModel>>(promotionCine);
            IList<PromotionCineViewModel> allPromotionCine = promotionCineVm as IList<PromotionCineViewModel>;
            IList<PromotionCineViewModel> listPromotionCine = new List<PromotionCineViewModel>();
            foreach (var itemPromotion in promotionVm as IList<PromotionViewModel>)
            {
                for(int i = 0; i < allPromotionCine.Count; i++)
                {
                    if (allPromotionCine[i].PromotionID == itemPromotion.PromotionID)
                        listPromotionCine.Add(allPromotionCine[i]);
                }
                //System.Diagnostics.Debug.WriteLine(itemPromotion.PromotionHeader);
            }
            ViewData["PromotionCineList"] = listPromotionCine;

            var cinema = _cinemaService.GetAll();
            var cinemaVm = Mapper.Map<IEnumerable<CinemaViewModel>>(cinema);
            IList<CinemaViewModel> allCinema = cinemaVm as IList<CinemaViewModel>;
            IList<CinemaViewModel> listCinema = new List<CinemaViewModel>();
            foreach (var itemCinema in allCinema)
            {
                for (int i = 0; i < listPromotionCine.Count; i++)
                {
                    if (listPromotionCine[i].CinemaID == itemCinema.CinemaID)
                        if (listCinema.Contains(itemCinema) == false) listCinema.Add(itemCinema);
                }
            }
            ViewData["CinemaList"] = listCinema;

            return View(promotionVm);
        }
        #endregion

    }
}