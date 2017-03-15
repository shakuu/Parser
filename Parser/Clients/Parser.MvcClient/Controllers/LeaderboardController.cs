using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    public class LeaderboardController : Controller
    {
        public LeaderboardController()
        {

        }

        [HttpGet]
        public ActionResult Damage()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Damage(int? page)
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
        public ActionResult Healing(int? page)
        {
            return this.Content("asd");
        }
    }
}