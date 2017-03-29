using System.Web.Mvc;

using Bytes2you.Validation;

using Parser.Common.Utilities.Contracts;
using Parser.Data.Services.Contracts;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Controllers
{
    [Authorize]
    public class LiveController : LoggingController
    {
        private readonly ILiveService liveService;
        private readonly IIdentityProvider identityProvider;

        public LiveController(ILiveService liveService, IIdentityProvider identityProvider)
        {
            Guard.WhenArgument(liveService, nameof(ILiveService)).IsNull().Throw();
            Guard.WhenArgument(identityProvider, nameof(IIdentityProvider)).IsNull().Throw();

            this.liveService = liveService;
            this.identityProvider = identityProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var username = this.identityProvider.GetUsername();
            var viewModel = this.liveService.GetLiveStatisticsViewModel(username);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLiveCombatStatistics()
        {
            var username = this.identityProvider.GetUsername();
            var viewModel = this.liveService.GetLiveStatisticsViewModel(username);

            return this.PartialView("_LiveStatisticsViewModel", viewModel);
        }
    }
}