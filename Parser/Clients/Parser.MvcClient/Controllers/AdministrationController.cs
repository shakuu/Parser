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
        private const int OutputCacheDurationInSeconds = 60;

        private readonly IAdministrationService administrationService;
        private readonly IAdministrationIndexViewModelFactory administrationIndexViewModelFactory;

        public AdministrationController(IAdministrationService administrationService, IAdministrationIndexViewModelFactory administrationIndexViewModelFactory)
        {
            this.administrationService = administrationService;
            this.administrationIndexViewModelFactory = administrationIndexViewModelFactory;
        }

        [HttpGet]
        [OutputCache(Duration = AdministrationController.OutputCacheDurationInSeconds, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult Index()
        {
            var viewModel = this.administrationIndexViewModelFactory.CreateAdministrationIndexViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DisplayLogEntries()
        {
            var viewModel = this.administrationService.GetLogEntriesForPeriod(24, MessageType.Error);

            return this.PartialView("_LogEntryViewModelPartial", viewModel);
        }

        [HttpPost]
        [OutputCache(Duration = AdministrationController.OutputCacheDurationInSeconds, VaryByParam = "periodType;messageType", Location = System.Web.UI.OutputCacheLocation.Any)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLogEntries(PeriodType periodType, MessageType messageType)
        {
            var viewModel = this.administrationService.GetLogEntriesForPeriod((int)periodType, messageType);

            return this.PartialView("_LogEntryViewModelPartial", viewModel);
        }
    }
}