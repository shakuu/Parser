using System.Web.Mvc;

using Parser.Common.Constants.Configuration;
using Parser.Data.Services.Contracts;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Roles = UserRoles.AdminRole)]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = this.administrationService.GetErrorsForPeriod(24);

            return View(viewModel);
        }
    }
}