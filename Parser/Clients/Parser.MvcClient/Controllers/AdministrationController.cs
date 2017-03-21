using System.Web.Mvc;

using Parser.Common.Constants.Configuration;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Roles = UserRoles.AdminRole)]
    public class AdministrationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}