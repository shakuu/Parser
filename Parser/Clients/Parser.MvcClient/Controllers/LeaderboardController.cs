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
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Damage(int? pageNumber)
        {
            return this.Content("asd");
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