using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaCinemaCommon.StatusCommon;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == CommonConstrants.SUCCESS_ALERT)
            {
                TempData["AlertType"] = "alert-success";
            }
            else
                if (type == CommonConstrants.WARNING_ALERT)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
                if (type == CommonConstrants.ERROR_ALERT)
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}