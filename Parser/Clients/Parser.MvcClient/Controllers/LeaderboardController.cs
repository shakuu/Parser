using System.Web.Mvc;

using Parser.Data.Services.Contracts;

namespace Parser.MvcClient.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardDamageService leaderboardDamageService;

        public LeaderboardController(ILeaderboardDamageService leaderboardDamageService)
        {
            this.leaderboardDamageService = leaderboardDamageService;
        }

        [HttpGet]
        public ActionResult Damage()
        {
            var viewModel = this.leaderboardDamageService.GetTopStoredCombatStatisticsOnPage(0);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Damage(int? pageNumber)
        {
            var viewModel = this.leaderboardDamageService.GetTopStoredCombatStatisticsOnPage(pageNumber.Value + 1);

            return this.PartialView("_DamageDonePerSecondViewModelsPartial", viewModel);
        }

        [HttpGet]
        public ActionResult Healing()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Healing(int? pageNumber)
        {
            return this.Content("asd");
        }
    }
}