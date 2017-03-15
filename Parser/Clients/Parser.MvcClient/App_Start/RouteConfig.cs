using System.Web.Mvc;
using System.Web.Routing;

namespace Parser.MvcClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "TopDps",
                url: "damage",
                defaults: new { controller = "Home", action = "Damage" }
            );

            routes.MapRoute(
                name: "TopHps",
                url: "healing",
                defaults: new { controller = "Home", action = "Healing" }
            );

            routes.MapRoute(
                name: "Live",
                url: "live",
                defaults: new { controller = "Live", action = "Index" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
