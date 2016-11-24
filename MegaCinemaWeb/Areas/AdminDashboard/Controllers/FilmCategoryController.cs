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
using System.Net;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmCategoryController : BaseController
    {
        IFilmCategoryService _filmCategoryService;
        public FilmCategoryController(IFilmCategoryService filmCategoryService)
        {
            _filmCategoryService = filmCategoryService;
        }

        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _filmCategoryService.GetFilmCategoryPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<FilmCategory>, IEnumerable<FilmCategoryViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<FilmCategoryViewModel>()
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

        #region #Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FilmCategoryViewModel filmCategory)
        {
            if (ModelState.IsValid)
            {
                //cập nhật thời gian, tên ảnh, người thực hiện, mã của sản phẩm - thiếu người thực hiện
                filmCategory.CreatedDate = filmCategory.UpdatedDate = DateTime.Now;
                //Phân quyền
                //filmCategory.CreatedBy = filmCategory.UpdatedBy = USER_ID;

                //thêm vào database và lưu kết quả
                FilmCategory result = new FilmCategory();
                result.UpdateFilmCategory(filmCategory);
                var resultFilmCategory = _filmCategoryService.Add(result);
                _filmCategoryService.SaveChanges();

                if (resultFilmCategory == null) return RedirectToAction("Index", "FilmCategory");
                else
                {
                    //fileUpload.SaveAs(pathImage);
                    SetAlert("Thêm thể loại phim thành công", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "FilmCategory");
                }
            }
            return View();
        }
        #endregion

        #region #Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                FilmCategory filmCategory = _filmCategoryService.Find((int)id);
                if (filmCategory == null)
                {
                    return HttpNotFound();
                }
                
                var resultVm = Mapper.Map<FilmCategory, FilmCategoryViewModel>(filmCategory);
                TempData["filmCategoryItem"] = resultVm;
                return View(resultVm);
            }
            return RedirectToAction("Index", "FilmCategory");
        }

        [HttpPost]
        public ActionResult Edit(FilmCategory filmCategory)
        {
            if (ModelState.IsValid)
            {
                //filmCategory.UpdatedBy               

                var result = (FilmCategoryViewModel)TempData["filmCategoryItem"];
                filmCategory.FilmCategoryID = result.FilmCategoryID;
                filmCategory.CreatedDate = result.CreatedDate;
                filmCategory.CreatedBy = result.CreatedBy;
                filmCategory.UpdatedDate = DateTime.Now;
                filmCategory.UpdatedBy = result.UpdatedBy;
                filmCategory.MetaDescription = result.MetaDescription;
                filmCategory.MetaKeyword = result.MetaKeyword;

                _filmCategoryService.Update(filmCategory);
                _filmCategoryService.SaveChanges();

                SetAlert("Sửa thể loại phim thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }
            return View(filmCategory);
        }
        #endregion

        #region #Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                FilmCategory filmCategory = _filmCategoryService.Find((int)id);
                if (filmCategory == null)
                {
                    return HttpNotFound();
                }
                _filmCategoryService.Delete(filmCategory);
                _filmCategoryService.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "FilmCategory");
        }
        #endregion
    }
}