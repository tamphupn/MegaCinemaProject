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
using MegaCinemaWeb.Infrastructure.Core;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FoodListController : BaseController
    {
        IFoodListService _foodListService;
        IStatusService _statusService;
        public FoodListController(IFoodListService foodListService, IStatusService statusService)
        {
            _foodListService = foodListService;
            _statusService = statusService;
        }
        // GET: AdminDashboard/FoodList
        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _foodListService.GetFoodListPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<FoodList>, IEnumerable<FoodListViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<FoodListViewModel>()
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
                    filePathSave = Path.GetFileName(fileUpload.FileName);
                    pathImage = Path.Combine(Server.MapPath("~/Content/FoodList"), filePathSave);
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
                if (resultFoodList == null) return RedirectToAction("Index", "Home");
                else
                {
                    if (filePathSave != "404.png")
                        fileUpload.SaveAs(pathImage);
                    _foodListService.SaveChanges();
                    SetAlert("Thêm món ăn thành công", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "FoodList");
                }
            }
            ViewBag.FoodStatusID = new SelectList(_statusService.GetAll(), "StatusID", "StatusName");
            return View(foodList);
        }
    }
}