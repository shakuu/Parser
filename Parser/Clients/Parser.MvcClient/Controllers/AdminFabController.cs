using System.Web.Mvc;

using Parser.Data.ViewModels.Factories;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        private const string OwnerUserName = "myuser@user.com";
        private const string AdminRole = "Admin";

        private readonly IAdminFabViewModelFactory adminFabViewModelFactory;

        public AdminFabController(IAdminFabViewModelFactory adminFabViewModelFactory)
        {
            this.adminFabViewModelFactory = adminFabViewModelFactory;
        }

        [ChildActionOnly]
        public ActionResult DisplayAdminFab()
        {
            var isOwnerAccount = HttpContext.User.Identity.Name == AdminFabController.OwnerUserName;
            var isAdminRole = HttpContext.User.IsInRole(AdminFabController.AdminRole);

            var viewModel = this.adminFabViewModelFactory.CreateAdminFabViewModel(isOwnerAccount, isAdminRole);

            return this.PartialView("_AdminFabPartial", viewModel);
        }
    }
}