using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaService;
using AutoMapper;
using MegaCinemaWeb.Models;
using System.IO;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaWeb.Infrastructure.Extensions;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FoodListController : Controller
    {
        IFoodListService _foodListService;
        IStatusService _statusService;
        public FoodListController(IFoodListService foodListService, IStatusService statusService)
        {
            _foodListService = foodListService;
            _statusService = statusService;
        }
        // GET: AdminDashboard/FoodList
        public ActionResult Index()
        {
            var result = Mapper.Map<IEnumerable<FoodListViewModel>>(_foodListService.GetAll());
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FoodStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodListViewModel foodList, HttpPostedFileBase fileUpload)
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
                    pathImage = Path.Combine(Server.MapPath("~/Content/FoodList"), fileName);
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
                //cập nhật thời gian, tên ảnh, người thực hiện, mã của sản phẩm - thiếu người thực hiện
                foodList.FoodPoster = filePathSave;
                foodList.CreatedDate = DateTime.Now;
                foodList.FoodPrefix = CommonConstrants.FOOD_PREFIX;
                //thêm vào database và lưu kết quả
                FoodList result = new FoodList();
                result.UpdateFoodList(foodList);
                var resultFoodList = _foodListService.Add(result);
                _foodListService.SaveChanges();

                if (resultFoodList == null) return RedirectToAction("Index", "Home");
                else
                {
                    fileUpload.SaveAs(pathImage);
                    return RedirectToAction("Index", "FoodList");
                }
            }
            return View();
        }
    }
}