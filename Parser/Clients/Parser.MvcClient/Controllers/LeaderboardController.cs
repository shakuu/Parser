using System.Web.Mvc;

using Bytes2you.Validation;

using Parser.Data.Services.Contracts;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Controllers
{
    public class LeaderboardController : LoggingController
    {
        private readonly ILeaderboardService leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            Guard.WhenArgument(leaderboardService, nameof(ILeaderboardService)).IsNull().Throw();
            
            this.leaderboardService = leaderboardService;
        }

        [HttpGet]
        public ActionResult Damage()
        {
            var viewModel = this.leaderboardService.GetTopDamageOnPage(0);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Damage(int? pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);

            var viewModel = this.leaderboardService.GetTopDamageOnPage(pageNumber.Value + 1);

            return this.PartialView("_DamageAjaxFormPartial", viewModel);
        }

        [HttpGet]
        public ActionResult Healing()
        {
            var viewModel = this.leaderboardService.GetTopHealingOnPage(0);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Healing(int? pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);

            var viewModel = this.leaderboardService.GetTopHealingOnPage(pageNumber.Value + 1);

            return this.PartialView("_HealingAjaxFormPartial", viewModel);
        }

        private int? ValidatePageNumber(int? pageNumber)
        {
            if (!pageNumber.HasValue)
            {
                pageNumber = 0;
            }
            else if (pageNumber == int.MaxValue)
            {
                pageNumber = 0;
            }
            else if (pageNumber < 0)
            {
                pageNumber = 0;
            }

            return pageNumber;
        }
    }
}