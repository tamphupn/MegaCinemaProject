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
                "AdminDashboard_default",
                "AdminDashboard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MegaCinemaWeb.Areas.AdminDashboard.Controllers" }
            );
        }
    }
}