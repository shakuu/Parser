using System.Web.Optimization;

namespace Parser.MvcClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/infinite-scroll-event").Include(
                        "~/Scripts/Leaderboard/infinite-scroll-event.js"));

            bundles.Add(new ScriptBundle("~/bundles/live-update").Include(
                        "~/Scripts/Live/timer-update.js"));

            bundles.Add(new ScriptBundle("~/bundles/owner").Include(
                        "~/Scripts/Live/timer-update.js",
                        "~/Scripts/Owner/promote.js",
                        "~/Scripts/Owner/demote.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-materialize").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/materialize/materialize.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/materialize/css/materialize.css"));

            bundles.Add(new StyleBundle("~/Content/leaderboard-css").Include(
                      "~/Content/leaderboard/leaderboard.css"));

            bundles.Add(new StyleBundle("~/Content/home-css").Include(
                      "~/Content/home/home-css.css"));

            bundles.Add(new StyleBundle("~/Content/footer-css").Include(
                      "~/Content/footer/footer-css.css"));
        }
    }
}
