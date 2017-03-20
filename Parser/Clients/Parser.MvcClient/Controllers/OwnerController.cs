using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Users = "myuser@user.com")]
    public class OwnerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}