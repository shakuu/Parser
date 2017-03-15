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
            return View();
        }

        [HttpGet]
        public ActionResult Healing()
        {
            return View();
        }
    }
}