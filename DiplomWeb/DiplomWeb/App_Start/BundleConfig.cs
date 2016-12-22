using System.Web;
using System.Web.Optimization;

namespace DiplomWeb
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                        "~/Scripts/tinymce.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/scripts.js"));
            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                    "~/Scripts/moment.js",
                    "~/Scripts/fullcalendar.js",
                    "~/Scripts/fullcalendar/locale/ru.js"
               
                    ));
            bundles.Add(new ScriptBundle("~/main").Include(
                  "~/Scripts/main.js"

                   ));
           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/calendar").Include(
                    "~/Content/fullcalendar.css"));
            bundles.Add(new StyleBundle("~/Content/styles").Include(
                   "~/Content/styles.css"));
            bundles.Add(new StyleBundle("~/Content/tabs").Include("~/Content/tabs.css"));
        }
    }
}
