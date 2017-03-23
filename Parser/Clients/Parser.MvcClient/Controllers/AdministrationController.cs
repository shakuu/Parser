using System.Web.Mvc;

using Parser.Common.Constants.Configuration;
using Parser.Common.Logging;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Administration;

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
            var viewModel = new AdministrationIndexViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DisplayLogEntries()
        {
            var viewModel = this.administrationService.GetErrorsForPeriod(24);

            return this.PartialView("_LogEntryViewModelPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLogEntries(PeriodType periodType, MessageType messageType)
        {
            var viewModel = this.administrationService.GetErrorsForPeriod((int)periodType);

            return this.PartialView("_LogEntryViewModelPartial", viewModel);
        }
    }
}