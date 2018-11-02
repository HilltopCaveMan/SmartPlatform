using System.Web;
using System.Web.Optimization;

namespace SuperGPS
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                             "~/assets/javascripts/jquery.min.js",
                             "~/assets/javascripts/jquery.easyui.min.js",
                             "~/assets/javascripts/datagrid-detailview.js",
                             "~/assets/javascripts/datagrid-filter.js",
                             "~/assets/Highcharts-4.0.3/js/highcharts.js",
                             "~/assets/Highcharts-4.0.3/js/modules/exporting.js",
                             "~/assets/javascripts/easyui-lang-zh_CN.js"
                             ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                             "~/assets/themes/metro-gray/easyui.css",
                             "~/assets/themes/icon.css",
                              "~/assets/themes/color.css",
                             "~/assets/themes/demo.css"                         
                            ));

        }
    }
}
