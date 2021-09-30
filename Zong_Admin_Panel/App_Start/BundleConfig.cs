using System.Web;
using System.Web.Optimization;

namespace Zong_Admin_Panel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/jquery").Include(
                      "~/Content/assets/js/plugin/jquery-ui-1.12.1.custom/jquery-ui.min.js",
                      "~/Content/assets/js/plugin/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js",
                      "~/Content/default_theme/assets/vendor/jquery-datatable/jquery.dataTables.min.js",
                        //"~/Content/default_theme/assets/vendor/jquery-datatable/dataTables.bootstrap.min.js",
                         "~/Content/default_theme/assets/vendor/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                        "~/Content/default_theme/assets/vendor/jquery-slimscroll/jquery.slimscroll.min.js"
                       
                        ));

            bundles.Add(new ScriptBundle("~/Content/script").Include(
                      "~/Content/assets/js/core/jquery.3.2.1.min.js",
                      "~/Content/assets/js/core/popper.min.js",
                      "~/Content/assets/js/core/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/default_theme/assets/vendor/bootstrap/css/bootstrap.min.css",
                 "~/Content/default_theme/assets/vendor/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                 "~/Content/default_theme/assets/vendor/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                 "~/Content/default_theme/assets/vendor/jquery-datatable/jquery.dataTables.min.css",
                 "~/Content/default_theme/assets/vendor/font-awesome/css/font-awesome.min.css",
                   "~/Content/assets/css/bootstrap.min.css",
                   "~/Content/assets/css/atlantis.css",
                   "~/Content/assets/css/bootstrap-select.css"
                   ));

            bundles.Add(new ScriptBundle("~/Content/common").Include(
                      "~/Content/assets/js/plugin/bootstrap-toggle/bootstrap-toggle.min.js",
                      "~/Content/assets/js/plugin/jquery-scrollbar/jquery.scrollbar.min.js",
                      "~/Content/assets/js/plugin/sortable/sortable.min.js",
                      "~/Content/assets/js/atlantis.min.js",
                      "~/Content/assets/js/setting-demo.js",
                      "~/Content/default_theme/assets/vendor/metisMenu/metisMenu.js",
                        "~/Content/default_theme/assets/vendor/toastr/toastr.js",
                        "~/Content/default_theme/assets/scripts/common.js",
                        "~/Content/default_theme/assets/scripts/CommonMethods.js",
                        "~/Scripts/ProjectScripts/CrudScript.js",
                        "~/Content/default_theme/assets/scripts/bootstrap-select.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/font").Include(
                      "~/Content/assets/js/plugin/webfont/webfont.min.js"));
        }
    }
}
