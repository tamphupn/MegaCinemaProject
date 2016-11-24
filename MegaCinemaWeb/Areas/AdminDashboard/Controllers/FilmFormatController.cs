using AutoMapper;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Infrastructure.Core;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmFormatController : BaseController
    {
        IFilmFormatService _filmFormatService;
        public FilmFormatController(IFilmFormatService filmFormatService)
        {
            _filmFormatService = filmFormatService;
        }

        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _filmFormatService.GetFilmFormatPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<FilmFormat>, IEnumerable<FilmFormatViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<FilmFormatViewModel>()
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
    }
}