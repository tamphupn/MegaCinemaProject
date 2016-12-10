using MegaCinemaService;
using MegaCinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaModel.Models;
using MegaCinemaWeb.Infrastructure.Extensions;
using MegaCinemaCommon.StatusCommon;
using AutoMapper;
using MegaCinemaWeb.Infrastructure.Core;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class PromotionController : BaseController
    {
        IPromotionService _promotionService;
        IStatusService _statusService;

        public PromotionController(IPromotionService promotionService, IStatusService statusService)
        {
            _promotionService = promotionService;
            _statusService = statusService;
        }

        // GET: AdminDashboard/Promotion
        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _promotionService.GetPromotionPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PromotionViewModel>()
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

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PromotionStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PromotionViewModel promotionVm, HttpPostedFileBase fileUpload)
        {
            string pathImage = string.Empty;
            //kiểm tra và thêm dữ liệu vào database
            if (ModelState.IsValid)
            {
                string filePathSave = string.Empty;
                //check image resource
                if (fileUpload == null)
                {
                    //đặt đường dẫn ảnh mặc định
                    filePathSave = "404.png";
                }
                else
                {
                    filePathSave = Path.GetFileName(fileUpload.FileName);
                    pathImage = Path.Combine(Server.MapPath("~/Content/Promotion"), filePathSave);
                    if (System.IO.File.Exists(pathImage))
                    {
                        //Hình ảnh đã tồn tại
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //Lưu tên file sẽ insert vào                    
                        filePathSave = fileUpload.FileName;
                    }

                }
                //update model từ view lên controller 
                //cập nhật thời gian, tên ảnh, người thực hiện, mã của sản phẩm - thiếu người thực hiện
                promotionVm.PromotionPoster = filePathSave;

                promotionVm.CreatedDate = DateTime.Now;
                Promotion result = new Promotion();
                result.UpdatePromotion(promotionVm);
                var resultPromotion = _promotionService.Add(result);
                _promotionService.SaveChanges();
                if (resultPromotion == null) return RedirectToAction("Index", "Home");
                else
                {
                    if (filePathSave != "404.png")
                        fileUpload.SaveAs(pathImage);
                    _promotionService.SaveChanges();

                    SetAlert("Thêm ưu đãi thành công!", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "Promotion");
                }
            }
            ViewBag.PromotionStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View(promotionVm);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Promotion promotion = _promotionService.Find((int)id);
                if (promotion == null)
                {
                    return HttpNotFound();
                }

                var resultVm = Mapper.Map<Promotion, PromotionViewModel>(promotion);
                TempData["promotionItem"] = resultVm;
                
                ViewBag.PromotionStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName", resultVm.PromotionStatusID);
                return View(resultVm);
            }
            return RedirectToAction("Index", "Promotion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PromotionViewModel promotionVm, HttpPostedFileBase fileUpload, bool checkEditPoster = false)
        {
            if (ModelState.IsValid)
            {
                var result = (PromotionViewModel)TempData["promotionItem"];
                promotionVm.PromotionID = result.PromotionID;
                promotionVm.CreatedDate = result.CreatedDate;
                promotionVm.CreatedBy = result.CreatedBy;
                promotionVm.UpdatedDate = DateTime.Now;
                promotionVm.UpdatedBy = result.UpdatedBy;
                promotionVm.MetaDescription = result.MetaDescription;
                promotionVm.MetaKeyword = result.MetaKeyword;


                if (checkEditPoster)
                {
                    // Lưu hình ảnh vào thư mục ~/Content/Promotion
                    string pathImage = string.Empty;
                    string filePathSave = string.Empty;
                    if (fileUpload == null)
                    {
                        filePathSave = "404.png";
                    }
                    else
                    {
                        filePathSave = Path.GetFileName(fileUpload.FileName);
                        pathImage = Path.Combine(Server.MapPath("~/Content/Promotion"), filePathSave);
                        if (System.IO.File.Exists(pathImage))
                        {
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            filePathSave = fileUpload.FileName;
                        }
                    }
                    if (filePathSave != "404.png")
                        fileUpload.SaveAs(pathImage);
                    promotionVm.PromotionPoster = filePathSave;
                }
                else
                {
                    promotionVm.PromotionPoster = result.PromotionPoster;
                }


                Promotion promotionUpdate = new Promotion();
                promotionUpdate.UpdatePromotion(promotionVm);
                promotionUpdate.PromotionID = result.PromotionID;

                _promotionService.Update(promotionUpdate);
                _promotionService.SaveChanges();

                SetAlert("Sửa ưu đãi thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }
            return View(promotionVm);
        }
        #endregion

        #region #Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Promotion promotion = _promotionService.Find((int)id);
                if (promotion == null)
                {
                    return HttpNotFound();
                }
                _promotionService.Delete(promotion);
                _promotionService.SaveChanges();
                SetAlert("Xóa 1 ưu đãi phim thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Promotion");
        }
        #endregion
    }
}