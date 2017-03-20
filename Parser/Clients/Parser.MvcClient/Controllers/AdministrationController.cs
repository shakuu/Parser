using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}