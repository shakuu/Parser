using System.Web.Mvc;

using Parser.Auth.Contracts;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Users = "myuser@user.com")]
    public class OwnerController : Controller
    {
        private const int OutputCacheDurationInSeconds = 300;

        private readonly IAuthOwnerService authOwnerService;

        public OwnerController(IAuthOwnerService authOwnerService)
        {
            this.authOwnerService = authOwnerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(1);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Promote(string username, int pageNumber)
        {
            this.authOwnerService.AddRoleAdmin(username);
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Demote(string username, int pageNumber)
        {
            this.authOwnerService.RemoveRoleAdmin(username);
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsersOnPage(int pageNumber)
        {
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber + 1);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }
    }
}