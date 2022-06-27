using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content").Include(
                 "~/Content/bootstrap.min.css",
                 "~/Content/lightbox.css",
                 "~/Content/DataTables/css/jquery.dataTables.css"
             ));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                "~/Scripts/jquery-3.6.0.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/lightbox-2.6.js",
                "~/Scripts/DataTables/jquery.dataTables.js"
            ));
        }
    }
}