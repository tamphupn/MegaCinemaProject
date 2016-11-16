using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using MegaCinemaWeb.Mappings;
using MegaCinemaWeb.App_Start;
using System.Web.Optimization;
using System.Web.Routing;

namespace MegaCinemaWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //register 
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfiguration.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
