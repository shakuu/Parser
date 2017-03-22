using System.Web.Mvc;

using Parser.Data.Services.Contracts;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Controllers
{
    public class LeaderboardController : LoggingController
    {
        private const int OutputCacheDurationInSeconds = 60;

        private readonly ILeaderboardDamageService leaderboardDamageService;
        private readonly ILeaderboardHealingService leaderboardHealingService;

        public LeaderboardController(ILeaderboardDamageService leaderboardDamageService, ILeaderboardHealingService leaderboardHealingService)
        {
            this.leaderboardDamageService = leaderboardDamageService;
            this.leaderboardHealingService = leaderboardHealingService;
        }

        [HttpGet]
        [OutputCache(Duration = LeaderboardController.OutputCacheDurationInSeconds, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult Damage()
        {
            var viewModel = this.leaderboardDamageService.GetTopStoredDamageOnPage(0);

            return this.View(viewModel);
        }

        [HttpPost]
        [OutputCache(Duration = LeaderboardController.OutputCacheDurationInSeconds, VaryByParam = "pageNumber", Location = System.Web.UI.OutputCacheLocation.Any)]
        [ValidateAntiForgeryToken]
        public ActionResult Damage(int? pageNumber)
        {
            var viewModel = this.leaderboardDamageService.GetTopStoredDamageOnPage(pageNumber.Value + 1);

            return this.PartialView("_DamageDonePerSecondViewModelsPartial", viewModel);
        }

        [HttpGet]
        [OutputCache(Duration = LeaderboardController.OutputCacheDurationInSeconds, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult Healing()
        {
            var viewModel = this.leaderboardHealingService.GetTopStoredHealingOnPage(0);

            return this.View(viewModel);
        }

        [HttpPost]
        [OutputCache(Duration = LeaderboardController.OutputCacheDurationInSeconds, VaryByParam = "pageNumber", Location = System.Web.UI.OutputCacheLocation.Any)]
        [ValidateAntiForgeryToken]
        public ActionResult Healing(int? pageNumber)
        {
            var viewModel = this.leaderboardHealingService.GetTopStoredHealingOnPage(pageNumber.Value + 1);

            return this.PartialView("_HealingDonePerSecondViewModelsPartial", viewModel);
        }
    }
}