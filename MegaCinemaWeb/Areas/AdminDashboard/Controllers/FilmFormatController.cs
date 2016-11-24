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
using MegaCinemaWeb.Infrastructure.Extensions;


namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmFormatController : BaseController
    {
        IFilmFormatService _filmFormatService;
        public FilmFormatController(IFilmFormatService filmFormatService)
        {
            _filmFormatService = filmFormatService;
        }

        #region #List_FilmFormat
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
        #endregion

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FilmFormatViewModel filmFormat)
        {
            if (ModelState.IsValid)
            {
                //cập nhật thời gian, tên ảnh, người thực hiện, mã của sản phẩm - thiếu người thực hiện
                filmFormat.CreatedDate = filmFormat.UpdatedDate = DateTime.Now;
                //Phân quyền
                //filmCategory.CreatedBy = filmCategory.UpdatedBy = USER_ID;

                //thêm vào database và lưu kết quả
                FilmFormat result = new FilmFormat();
                result.UpdateFilmFormat(filmFormat);
                var resultFilmFormat = _filmFormatService.Add(result);
                _filmFormatService.SaveChanges();

                if (resultFilmFormat == null) return RedirectToAction("Index", "FilmFormat");
                else
                {
                    //fileUpload.SaveAs(pathImage);
                    SetAlert("Thêm thể định dạng phim thành công", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "FilmFormat");
                }
            }
            return View();
        }

    }
}