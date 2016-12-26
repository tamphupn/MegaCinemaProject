using System.Web.Mvc;

namespace MegaCinemaWeb.Areas.AdminDashboard
{
    public class AdminDashboardAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminDashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Staff insert",
                url: "them-nhan-vien",
                defaults: new { action = "StaffRegister", controller = "Account", id = UrlParameter.Optional },
                namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            );

            //context.MapRoute(
            //    name: "Film Category",
            //    url: "danh-sach-the-loai-phim/{id}",
            //    defaults: new { action = "Index", controller = "FilmCategory", id = UrlParameter.Optional },
            //    namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            //);

            context.MapRoute(
                name: "Food List Detail",
                url: "danh-muc-mon-an/{id}",
                defaults: new { action = "Index", controller = "FoodList", id = UrlParameter.Optional },
                namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            );

            context.MapRoute(
                "AdminDashboard_login",
                "AdminDashboard",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            );

            context.MapRoute(
                "AdminDashboard_default",
                "AdminDashboard/{controller}/{action}/{id}",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            );

            
        }
    }
}