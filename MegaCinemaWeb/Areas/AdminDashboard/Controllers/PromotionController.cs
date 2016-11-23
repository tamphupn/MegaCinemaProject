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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PromotionStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PromotionViewModel promotion, HttpPostedFileBase fileUpload)
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
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    pathImage = Path.Combine(Server.MapPath("~/Content/Promotion"), fileName);
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
                promotion.PromotionPoster = filePathSave;
                promotion.CreatedDate = DateTime.Now;
                Promotion result = new Promotion();
                result.UpdatePromotion(promotion);
                var resultPromotion = _promotionService.Add(result);
                _promotionService.SaveChanges();
                if (resultPromotion == null) return RedirectToAction("Index", "Home");
                else
                {
                    fileUpload.SaveAs(pathImage);
                    SetAlert("Thêm món ăn thành công", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "FoodList");
                }
            }
            ViewBag.PromotionStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View(promotion);
        }
    }
}