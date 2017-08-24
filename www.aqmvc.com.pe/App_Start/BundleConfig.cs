using System.Web;
using System.Web.Optimization;

namespace www.aqmvc.com.pe
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //// preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                                "~/Scripts/bootstrap-select.js",
                                "~/Scripts/bootstrap-select.min.js",
                                "~/Scripts/script-bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/plugins/jQuery/jquery-2.2.3.min.js",
                      "~/Content/jquery/jquery-ui.min.js",

                      "~/bootstrap/js/bootstrap.min.js",

                      "~/jquery/raphael-min.js",
                      "~/plugins/morris/morris.min.js",
                      "~/plugins/sparkline/jquery.sparkline.min.js",
                      "~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/plugins/knob/jquery.knob.js",
                      "~/Content/jquery/moment.min.js",
                      "~/plugins/daterangepicker/daterangepicker.js",
                      "~/plugins/datepicker/bootstrap-datepicker.js",
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/plugins/fastclick/fastclick.js",
                      "~/dist/js/app.min.js",

                       //"~/dist/js/pages/dashboard.js",

                       "~/Scripts/dist/js/demo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bootstrap/css/bootstrap.min.css",
                      "~/Content/toastr.css",
                      "~/Content/lib/ionicons.min.css",
                      "~/dist/css/AdminLTE.min.css",
                      "~/dist/css/skins/_all-skins.min.css",
                      "~/plugins/iCheck/flat/blue.css",
                      "~/plugins/morris/morris.css",
                      "~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                      "~/plugins/datepicker/datepicker3.css",
                      "~/plugins/daterangepicker/daterangepicker.css",
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"));

            //bundles.Add(new StyleBundle("~/Content/Bootstrap-Select/css").Include(
            //                 "~/Content/bootstrap-select.css",
            //                 "~/Content/bootstrap-select.min.css"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js",
            //            "~/Scripts/jquery.unobtrusive-ajax.min.js",
            //            "~/Scripts/toastr.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
            //                     "~/Scripts/bootstrap-select.js",
            //                     "~/Scripts/bootstrap-select.min.js",
            //                     "~/Scripts/script-bootstrap-select.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/Bootstrap-Select/css").Include(
            //                    "~/Content/style/bootstrap-select.css",
            //                    "~/Content/style/bootstrap-select.min.css"));
        }
    }
}
