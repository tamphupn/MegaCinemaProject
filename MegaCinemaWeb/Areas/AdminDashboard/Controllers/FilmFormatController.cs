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

        #region #Create_FilmFormat
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
        #endregion

        #region #Edit_FilmFormat
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                FilmFormat filmFormat = _filmFormatService.Find((int)id);
                if (filmFormat == null)
                {
                    return HttpNotFound();
                }

                var resultVm = Mapper.Map<FilmFormat, FilmFormatViewModel>(filmFormat);
                TempData["filmFormatItem"] = resultVm;
                return View(resultVm);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FilmFormat filmFormat)
        {
            if (ModelState.IsValid)
            {
                //filmCategory.UpdatedBy
                var result = (FilmFormatViewModel)TempData["filmFormatItem"];
                filmFormat.FilmFormatID = result.FilmFormatID;
                filmFormat.UpdatedDate = DateTime.Now;
                filmFormat.UpdatedBy = result.UpdatedBy;
                filmFormat.MetaDescription = result.MetaDescription;
                filmFormat.MetaKeyword = result.MetaKeyword;

                _filmFormatService.Update(filmFormat);
                _filmFormatService.SaveChanges();

                SetAlert("Sửa định dạng phim thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }
            return View(filmFormat);
        }
        #endregion

        #region #Delete_FilmFormat
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                FilmFormat filmFormat = _filmFormatService.Find((int)id);
                if (filmFormat == null)
                {
                    return HttpNotFound();
                }
                _filmFormatService.Delete(filmFormat);
                _filmFormatService.SaveChanges();
                SetAlert("Xóa 1 định dạng phim thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "FilmFormat");
        }
        #endregion
    }
}