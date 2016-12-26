using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MegaCinemaCommon.StatusCommon;

namespace MegaCinemaWeb.Areas.AdminDashboard.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!session)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }

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