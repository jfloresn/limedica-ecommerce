using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace Web.Common.HttpApplications.AppConfig
{
    public class BundleConfig
    {
   
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/limedica").Include(
                        "~/Content/css/style_limedica.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                        "~/Content/css/styles.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                "~/Content/plugins/sweetalert/sweetalert.css"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Content/js/scripts.js", "~/Content/js/lazysizes.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js-home").Include(
                        "~/Content/js/slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/sweetalert").Include(
                        "~/Content/js/sweetalert/sweetalert.min.js"));

            // Para habilitar la minificación y compresión
            BundleTable.EnableOptimizations = true;

        }
    }
}