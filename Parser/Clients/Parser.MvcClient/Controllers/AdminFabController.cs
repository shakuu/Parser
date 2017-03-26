using System.Web.Mvc;

using Bytes2you.Validation;

using Parser.Common.Constants.Configuration;
using Parser.Common.Utilities.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        private readonly IIdentityProvider identityProvider;
        private readonly IAdminFabViewModelFactory adminFabViewModelFactory;

        public AdminFabController(IIdentityProvider identityProvider, IAdminFabViewModelFactory adminFabViewModelFactory)
        {
            Guard.WhenArgument(identityProvider, nameof(IIdentityProvider)).IsNull().Throw();
            Guard.WhenArgument(adminFabViewModelFactory, nameof(IAdminFabViewModelFactory)).IsNull().Throw();

            this.identityProvider = identityProvider;
            this.adminFabViewModelFactory = adminFabViewModelFactory;
        }

        [ChildActionOnly]
        public ActionResult DisplayAdminFab()
        {
            var isOwnerAccount = this.identityProvider.IsOwner();
            var isAdminRole = this.identityProvider.IsInRole(UserRoles.AdminRole);

            var viewModel = this.adminFabViewModelFactory.CreateAdminFabViewModel(isOwnerAccount, isAdminRole);

            return this.PartialView("_AdminFabPartial", viewModel);
        }
    }
}