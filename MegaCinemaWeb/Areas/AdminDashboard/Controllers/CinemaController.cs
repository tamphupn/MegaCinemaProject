using MegaCinemaService;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaWeb.Infrastructure.Extensions;
using MegaCinemaModel.Models;
using MegaCinemaCommon.StatusCommon;
using AutoMapper;
using MegaCinemaWeb.Infrastructure.Core;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    //[Authorize]
    public class CinemaController : Controller
    {
        //TEST
        ICinemaService _cinemaService;
        IStatusService _statusService;
       
        IStaffService _staffService;


        public CinemaController(ICinemaService cinemaService, IStatusService statusService, IStaffService staffService)
        {
            _cinemaService = cinemaService;
            _statusService = statusService;
            _staffService = staffService;
        }

        // GET: AdminDashboard/Cinema
        public ActionResult Index(int page = 0)
        {   //Show List Cinema in Database
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;

            int count = _cinemaService.GetAll().OfType<Cinema>().Count();

            var result = _cinemaService.GetCinemaPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<CinemaViewModel>()
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
            ViewBag.CinemaStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            List<Staff> listStaff = (List<Staff>)_staffService.GetAll().Where(x => x.StaffStatus == "AC").ToList();

   
            ViewBag.CinemaManagerID = new SelectList(_staffService.GetAll(), "StaffID", "StaffCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CinemaViewModel cinemaViewModel)
        {
            if (ModelState.IsValid)
            {
                Cinema cinema = new Cinema();
                cinemaViewModel.CreatedDate = DateTime.Now;
                cinema.UpdateCinema(cinemaViewModel);

                if (cinema != null)
                {
                    _cinemaService.Add(cinema);
                    _cinemaService.SaveChanges();

                    //Redirect To Index Action
                    //SetAlert("Thêm rạp chiếu phim thành công!", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index");
                }
            }
            //Action/Controller
            ViewBag.CinemaStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            //ViewBag.StaffId = new SelectList(_staffService.GetAll(), "StaffID", "StaffCode");
            return View(cinemaViewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Cinema cinema = _cinemaService.Find((int)id);
                _cinemaService.Delete(cinema);
                _cinemaService.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        public ActionResult Edit(int? id)
        {
            CinemaViewModel cinemaViewModel = null;
            if (id != null)
            {
                Cinema cinema  = _cinemaService.Find((int)id);
                cinema.CinemaID = (int)id;
                cinemaViewModel = Mapper.Map<Cinema, CinemaViewModel>(cinema);
            }

            if (cinemaViewModel != null)
            {
                ViewBag.CinemaManagerID = new SelectList(_staffService.GetAll(), "StaffID", "StaffCode", cinemaViewModel.CinemaManagerID);
                ViewBag.CinemaStatus = new SelectList(_statusService.GetAll(), "StatusID", "StatusName", cinemaViewModel.CinemaStatus);
                return View(cinemaViewModel);
            }
               
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CinemaViewModel viewModel)
        {
            Cinema cinema = new Cinema();
            viewModel.UpdatedDate = DateTime.Now;
            cinema.UpdateCinema(viewModel);

            _cinemaService.Update(cinema);
            _cinemaService.SaveChanges();
         
            return RedirectToAction("Index");
        }
    }
}