using System.Web.Mvc;

using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Controllers
{
    public class HomeController : LoggingController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}