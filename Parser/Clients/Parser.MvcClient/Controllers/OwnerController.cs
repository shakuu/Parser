using System.Web.Mvc;

using Parser.Auth.Contracts;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Users = "myuser@user.com")]
    public class OwnerController : Controller
    {
        private readonly IAuthOwnerService authOwnerService;

        public OwnerController(IAuthOwnerService authOwnerService)
        {
            this.authOwnerService = authOwnerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username)
        {
            return View();
        }
    }
}