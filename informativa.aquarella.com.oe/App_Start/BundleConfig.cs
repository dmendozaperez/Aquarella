using System.Web;
using System.Web.Optimization;

namespace informativa.aquarella.com.oe
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //// preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/jquery-1.11.1.min.js",
                      "~/assets/bootstrap/js/bootstrap.min.js",
                      "~/assets/js/bootstrap-hover-dropdown.min.js",
                      "~/assets/js/jquery.backstretch.min.js",
                      "~/assets/js/wow.min.js",
                      "~/assets/js/retina-1.1.0.min.js",
                      "~/assets/js/jquery.magnific-popup.min.js",
                      "~/assets/flexslider/jquery.flexslider-min.js",
                      "~/assets/js/jflickrfeed.min.js",
                      "~/assets/js/masonry.pkgd.min.js",                    
                      "~/assets/js/jquery.ui.map.min.js",
                      "~/assets/js/scripts.js"
                      ));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/bootstrap/css/bootstrap.min.css",
                      "~/assets/font-awesome/css/font-awesome.min.css",
                      "~/assets/css/animate.css",
                      "~/assets/css/magnific-popup.css",
                      "~/assets/flexslider/flexslider.css",
                      "~/assets/css/form-elements.css",
                      "~/assets/css/css1.css",
                      "~/assets/css/css2.css",
                      "~/assets/css/css3.css",
                      "~/assets/css/style.css",
                      "~/assets/css/camera.css",
                      "~/assets/css/media-queries.css"));
        }
    }
}
