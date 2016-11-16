using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MegaCinemaWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryClient").Include(
                "~/Content/scripts/Client/js/jquery.min.js",
                "~/Content/scripts/Client/js/index.js",
                "~/Content/scripts/Client/js/jquery.bxslider.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}