using System.Web.Mvc;

using Parser.Common.Constants.Configuration;
using Parser.Common.Logging;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.MvcClient.Controllers
{
    [Authorize(Roles = UserRoles.AdminRole)]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        private readonly IAdministrationIndexViewModelFactory administrationIndexViewModelFactory;

        public AdministrationController(IAdministrationService administrationService, IAdministrationIndexViewModelFactory administrationIndexViewModelFactory)
        {
            this.administrationService = administrationService;
            this.administrationIndexViewModelFactory = administrationIndexViewModelFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = this.administrationIndexViewModelFactory.CreateAdministrationIndexViewModel();

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