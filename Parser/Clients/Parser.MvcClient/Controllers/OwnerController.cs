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
        [OutputCache(Duration = OwnerController.OutputCacheDurationInSeconds, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult Index()
        {
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(1);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username)
        {
            this.authOwnerService.AddRoleAdmin(username);

            return this.PartialView("_AddRoleResultPartial");
        }

        [HttpPost]
        [OutputCache(Duration = OwnerController.OutputCacheDurationInSeconds, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsersOnPage(int pageNumber)
        {
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber + 1);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }
    }
}