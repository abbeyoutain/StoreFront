using System.Web.Optimization;

namespace StoreFront.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Content/js/jquery.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Content/js/jquery-migrate-3.0.1.min.js",
                "~/Content/js/popper.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/jquery.easing.1.3.js",
                "~/Content/js/jquery.waypoints.min.js",
                "~/Content/js/jquery.stellar.min.js",
                "~/Content/js/owl.carousel.min.js",
                "~/Content/js/jquery.magnific-popup.min.js",
                "~/Content/js/jquery.animateNumber.min.js",
                "~/Content/js/scrollax.min.js",
                "~/Content/js/google-map.js",
                "~/Content/js/main.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/animate.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/css/magnific-popup.css",
                      "~/Content/css/flaticon.css",
                      "~/Content/css/style.css"
                      ));

        }
    }
}
