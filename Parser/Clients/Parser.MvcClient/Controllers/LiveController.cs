using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    [Authorize]
    public class LiveController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}