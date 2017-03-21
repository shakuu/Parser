using System.Web.Mvc;

using Parser.Common.Constants.Configuration;
using Parser.Data.ViewModels.Factories;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        private readonly IAdminFabViewModelFactory adminFabViewModelFactory;

        public AdminFabController(IAdminFabViewModelFactory adminFabViewModelFactory)
        {
            this.adminFabViewModelFactory = adminFabViewModelFactory;
        }

        [ChildActionOnly]
        public ActionResult DisplayAdminFab()
        {
            var isOwnerAccount = HttpContext.User.Identity.Name == UserRoles.OwnerAccountName;
            var isAdminRole = HttpContext.User.IsInRole(UserRoles.AdminRole);

            var viewModel = this.adminFabViewModelFactory.CreateAdminFabViewModel(isOwnerAccount, isAdminRole);

            return this.PartialView("_AdminFabPartial", viewModel);
        }
    }
}