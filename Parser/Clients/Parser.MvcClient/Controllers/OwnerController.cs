using System.Web.Mvc;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Common.Constants.Configuration;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Users = UserRoles.OwnerAccountName)]
    public class OwnerController : Controller
    {
        private const int OutputCacheDurationInSeconds = 300;

        private readonly IAuthOwnerService authOwnerService;

        public OwnerController(IAuthOwnerService authOwnerService)
        {
            Guard.WhenArgument(authOwnerService, nameof(IAuthOwnerService)).IsNull().Throw();

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
        public ActionResult Promote(string username, int? pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);

            this.authOwnerService.AddRoleAdmin(username);
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber.Value);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Demote(string username, int? pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);

            this.authOwnerService.RemoveRoleAdmin(username);
            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber.Value);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsersOnPage(int? pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);

            var viewModel = this.authOwnerService.GetAuthUsersOnPage(pageNumber.Value + 1);

            return this.PartialView("_AuthUserViewModelsPartial", viewModel);
        }

        private int? ValidatePageNumber(int? pageNumber)
        {
            if (!pageNumber.HasValue)
            {
                pageNumber = 0;
            }
            else if (pageNumber == int.MaxValue)
            {
                pageNumber = 0;
            }
            else if (pageNumber < 0)
            {
                pageNumber = 0;
            }

            return pageNumber;
        }
    }
}