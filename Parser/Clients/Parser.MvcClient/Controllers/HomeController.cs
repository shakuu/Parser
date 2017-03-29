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

        public ActionResult Download()
        {
            return this.File("~/App_Data/Parser.zip", System.Net.Mime.MediaTypeNames.Application.Zip, "Parser.zip");
        }
    }
}