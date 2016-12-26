using AutoMapper;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaModel.Models;
using MegaCinemaService;
using MegaCinemaWeb.Infrastructure.Extensions;
using MegaCinemaWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaCommon.DataCrawler;
using MegaCinemaCommon.ANNModel;
using MegaCinemaWeb.Infrastructure.Core;
using Microsoft.AspNet.Identity;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class FilmController : BaseController
    {
        IFilmService _filmService;
        IStatusService _statusService;
        IFilmRattingService _filmRattingService;
        public FilmController(IFilmService filmService, IStatusService statusService, IFilmRattingService filmRattingService)
        {
            _filmService = filmService;
            _statusService = statusService;
            _filmRattingService = filmRattingService;
        }

        public ActionResult Index(int page = 0)
        {
            int pageSize = CommonConstrants.PAGE_SIZE;
            int totalRow = 0;
            var result = _filmService.GetFilmListPaging(page, pageSize, out totalRow);
            var resultVm = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmViewModel>>(result);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<FilmViewModel>()
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
            //Get list ratting 
            ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmViewModel newFilm, HttpPostedFileBase fileUpload)
        {
            string pathImage = string.Empty;
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
                    pathImage = Path.Combine(Server.MapPath("~/Content/FilmPoster"), filePathSave);
                    if (System.IO.File.Exists(pathImage))
                    {
                        //Hình ảnh đã tồn tại
                        ModelState.AddModelError("", "Hình ảnh đã tồn tại trong hệ thống, vui lòng chọn hình hoặc thay đổi tên hình ");
                        ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                        return View(newFilm);
                    }
                    else
                    {
                        //Lưu tên file sẽ insert vào                    
                        filePathSave = fileUpload.FileName;
                    }

                    //kiểm tra kích thước file
                    if (fileUpload.ContentLength / 1024 > 4096)
                    {
                        ModelState.AddModelError("", "Kích thước file lớn hơn 2MB ");
                        ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                        return View(newFilm);
                    }
                }

                //Kiểm tra ngày công chiếu và ngày dừng 
                var totalDay = (newFilm.FilmFinishPremiered - newFilm.FilmFirstPremiered).Value.Days;
                if (totalDay < 7)
                {
                    ModelState.AddModelError("", "Ngày dừng chiếu phải lớn hơn ngày công chiếu ít nhất 7 ngày ");
                    ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                    return View(newFilm);
                }

                //More information
                newFilm.CreatedDate = DateTime.Now;
                //string currentUserId = User.Identity.GetUserId();
                newFilm.CreatedBy = User.Identity.GetUserId();
                newFilm.FilmPrefix = CommonConstrants.FILM_PREFIX;
                newFilm.FilmPoster = filePathSave;
                newFilm.FilmStatus = StatusCommonConstrants.PENDING;

                //thêm vào database và lưu kết quả
                Film result = new Film();
                result.UpdateFilm(newFilm);
                var resultFilm = _filmService.Add(result);
                if (resultFilm == null) return RedirectToAction("Index", "Home");
                else
                {
                    if (filePathSave != "404.png")
                        fileUpload.SaveAs(pathImage);
                    _filmService.SaveChanges();

                    SetAlert("Thêm film mới: " + result.FilmName + " thành công", CommonConstrants.SUCCESS_ALERT);
                    return RedirectToAction("Index", "Film");
                }
            }
            ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");

            return View();
        }

        public ActionResult PreditionIndex()
        {
            return View();
        }
        private IMDBViewModel MappingDataMovie(IMDBModel model)
        {
            IMDBViewModel modelresult = new IMDBViewModel();
            modelresult.MovieName = model.MovieName;
            modelresult.MovieBudget = model.MovieBudget;
            modelresult.MovieDuration = model.MovieDuration;
            modelresult.MovieReleaseDate = model.MovieReleaseDate;
            modelresult.MovieGenre = model.MovieGenre[0];
            modelresult.MovieDirector = model.MovieDirector[0];
            modelresult.MovieLink = model.MovieLink;
            modelresult.MovieLinkPoster = model.MovieLinkPoster;
            modelresult.MovieLinkTrailer = model.MovieLinkTrailer;
            modelresult.MovieProduction = model.MovieProduction[0];
            modelresult.MovieMusicby = model.MovieMusicby[0];
            modelresult.MovieStarActor = model.MovieStarActor[0];
            modelresult.MovieWriter = model.MovieWriter[0];

            for (int i = 1; i < model.MovieGenre.Count; i++)
            {
                modelresult.MovieGenre += ", " + model.MovieGenre[i];
            }

            for (int i = 1; i < model.MovieDirector.Count; i++)
            {
                modelresult.MovieDirector += ", " + model.MovieDirector[i];
            }

            for (int i = 1; i < model.MovieProduction.Count; i++)
            {
                modelresult.MovieProduction += ", " + model.MovieProduction[i];
            }

            for (int i = 1; i < model.MovieMusicby.Count; i++)
            {
                modelresult.MovieMusicby += ", " + model.MovieMusicby[i];
            }

            for (int i = 1; i < model.MovieStarActor.Count; i++)
            {
                modelresult.MovieStarActor += ", " + model.MovieStarActor[i];
            }

            for (int i = 1; i < model.MovieWriter.Count; i++)
            {
                modelresult.MovieWriter += ", " + model.MovieWriter[i];
            }

            return modelresult;
        }
        [HttpPost]
        public ActionResult GetDetailFilmFromIMDB(string filmName)
        {
            if (filmName == null) return Json(new
            {
                result = "KO",
                value = "null",
            }, JsonRequestBehavior.AllowGet);

            //get data and serilize 
            IMDBModel result = IMDBModelHelper.GetMovieInformation(filmName);
            TempData["FilmUpdate"] = result;
            if (result == null) return Json(new
            {
                result = "KO",
                value = "null",
            }, JsonRequestBehavior.AllowGet);

            IMDBViewModel resultVm = new IMDBViewModel();

            resultVm = MappingDataMovie(result);
            return Json(new
            {
                result = "OK",
                value = resultVm,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FilmPredition(string filmName)
        {
            var resultFilm = (IMDBModel)TempData["FilmUpdate"];
            if (resultFilm == null || String.IsNullOrEmpty(filmName))
                return Json(new
                {
                    result = "KO",
                    value = "null",
                }, JsonRequestBehavior.AllowGet);

            //Predition area 
            IMDBModelHelper.FilmState team = IMDBModelHelper.StartTrain(resultFilm);

            if (team == IMDBModelHelper.FilmState.DEFAULT)
                return Json(new
                {
                    result = "KO",
                    value = "null",
                }, JsonRequestBehavior.AllowGet);

            //Train data
            return Json(new
            {
                result = "OK",
                value = team,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var result = _filmService.Find((int)id);
                if (result == null)
                {
                    return HttpNotFound();
                }

                var resultVm = Mapper.Map<Film, FilmViewModel>(result);
                TempData["filmItem"] = resultVm;
                ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");

                var listStatus = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Đang công chiếu", Value = "PEN"},
                    new SelectListItem {Text = "Sắp công chiếu", Value = "REL"},
                    new SelectListItem {Text = "Suất chiếu đặc biệt", Value = "REW"},
                };

                foreach (var item in listStatus)
                {
                    if (item.Value == resultVm.FilmStatus)
                    {
                        item.Selected = true;
                        break;
                    }
                }

                ViewBag.FilmStatusID = new SelectList(listStatus, "Value", "Text");

                return View(resultVm);
            }
            return RedirectToAction("Index", "Film");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FilmViewModel filmVm, HttpPostedFileBase fileUpload, bool checkEditPoster = false)
        {
            if (ModelState.IsValid)
            {
                var result = (FilmViewModel)TempData["filmItem"];
                filmVm.FilmID = result.FilmID;
                filmVm.CreatedDate = result.CreatedDate;
                filmVm.CreatedBy = result.CreatedBy;
                filmVm.UpdatedDate = DateTime.Now;
                filmVm.UpdatedBy = User.Identity.GetUserId();
                filmVm.MetaDescription = result.MetaDescription;
                filmVm.MetaKeyword = result.MetaKeyword;
                filmVm.FilmPrefix = CommonConstrants.FILM_PREFIX;

                string pathImage = string.Empty;
                if (checkEditPoster)
                {
                    // Lưu hình ảnh vào thư mục ~/Content/Promotion                   
                    string filePathSave = string.Empty;
                    if (fileUpload == null)
                    {
                        //Nếu người dùng nhấn sửa nhưng không sửa
                        filePathSave = result.FilmPoster;
                        filmVm.FilmPoster = filePathSave;
                    }
                    else
                    {
                        filePathSave = Path.GetFileName(fileUpload.FileName);
                        pathImage = Path.Combine(Server.MapPath("~/Content/FilmPoster"), filePathSave);
                        if (System.IO.File.Exists(pathImage))
                        {
                            ModelState.AddModelError("", "Hình ảnh đã tồn tại trong hệ thống, vui lòng chọn hình hoặc thay đổi tên hình ");
                            var listStatus = new List<SelectListItem>
                            {
                                new SelectListItem {Text = "Đang công chiếu", Value = "PEN"},
                                new SelectListItem {Text = "Sắp công chiếu", Value = "REL"},
                                new SelectListItem {Text = "Suất chiếu đặc biệt", Value = "REW"},
                            };

                            foreach (var item in listStatus)
                            {
                                if (item.Value == filmVm.FilmStatus)
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }

                            ViewBag.FilmStatusID = new SelectList(listStatus, "Value", "Text");
                            ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                            return View(filmVm);
                        }
                        else
                        {
                            filePathSave = fileUpload.FileName;
                        }

                        //kiểm tra kích thước file
                        if (fileUpload.ContentLength / 1024 > 4096)
                        {
                            ModelState.AddModelError("", "Kích thước file lớn hơn 2MB ");
                            var listStatus = new List<SelectListItem>
                            {
                                new SelectListItem {Text = "Đang công chiếu", Value = "PEN"},
                                new SelectListItem {Text = "Sắp công chiếu", Value = "REL"},
                                new SelectListItem {Text = "Suất chiếu đặc biệt", Value = "REW"},
                            };

                            foreach (var item in listStatus)
                            {
                                if (item.Value == filmVm.FilmStatus)
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }

                            ViewBag.FilmStatusID = new SelectList(listStatus, "Value", "Text");
                            ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                            return View(filmVm);
                        }

                        filmVm.FilmPoster = filePathSave;
                    }


                }
                else
                {
                    filmVm.FilmPoster = result.FilmPoster;
                }

                //Kiểm tra ngày công chiếu và ngày dừng 
                var totalDay = (filmVm.FilmFinishPremiered - filmVm.FilmFirstPremiered).Value.Days;
                if (totalDay < 7)
                {
                    ModelState.AddModelError("", "Ngày dừng chiếu phải lớn hơn ngày công chiếu ít nhất 7 ngày ");
                    var listStatus = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Đang công chiếu", Value = "PEN"},
                        new SelectListItem {Text = "Sắp công chiếu", Value = "REL"},
                        new SelectListItem {Text = "Suất chiếu đặc biệt", Value = "REW"},
                    };

                    foreach (var item in listStatus)
                    {
                        if (item.Value == filmVm.FilmStatus)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    ViewBag.FilmStatusID = new SelectList(listStatus, "Value", "Text");
                    ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
                    return View(filmVm);
                }


                Film film = new Film();
                film.UpdateFilm(filmVm);
                film.FilmID = result.FilmID;

                _filmService.Update(film);
                _filmService.SaveChanges();
                if (film.FilmPoster != "404.png" && !String.IsNullOrEmpty(pathImage))
                    fileUpload.SaveAs(pathImage);

                SetAlert("Sửa thông tin phim: " + film.FilmName + " thành công", CommonConstrants.SUCCESS_ALERT);
                return RedirectToAction("Index");
            }

            var listStatus1 = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "Đang công chiếu", Value = "PEN"},
                        new SelectListItem {Text = "Sắp công chiếu", Value = "REL"},
                        new SelectListItem {Text = "Suất chiếu đặc biệt", Value = "REW"},
                    };

            foreach (var item in listStatus1)
            {
                if (item.Value == filmVm.FilmStatus)
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.FilmStatusID = new SelectList(listStatus1, "Value", "Text");
            ViewBag.FilmRattingID = new SelectList(_filmRattingService.GetAll(), "RatingID", "RatingDescription");
            return View(filmVm);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var film = _filmService.Find((int)id);
                if (film == null)
                {
                    return HttpNotFound();
                }
                var result = _filmService.Delete(film);
                if (result != null)
                {
                    _filmService.SaveChanges();
                    SetAlert("Xóa phim thành công", CommonConstrants.SUCCESS_ALERT);

                    //remove map path 
                    string pathImage = Path.Combine(Server.MapPath("~/Content/FilmPoster"), result.FilmPoster);
                    System.IO.File.Delete(pathImage);

                    return RedirectToAction("Index");
                }
                SetAlert("Xóa phim thất bại", CommonConstrants.WARNING_ALERT);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Promotion");
        }
    }
}