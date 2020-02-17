using System.Web;
using System.Web.Optimization;

namespace Car
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*", "~/Scripts/Dialog.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
          "~/Content/bootstrap/css/bootstrap.css",
          "~/Content/bootstrap/css/bootstrap.css.map",
          "~/Content/bootstrap/css/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/Dialog").Include(
                                       "~/Scripts/Dialog.js"));
            
        }
    }
}
