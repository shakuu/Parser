using System.Web.Mvc;

using Parser.Data.Services.Contracts;

namespace Parser.MvcClient.Controllers
{
    [Authorize]
    public class LiveController : Controller
    {
        private readonly ILiveService liveService;

        public LiveController(ILiveService liveService)
        {
            this.liveService = liveService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = this.liveService.GetLiveStatisticsViewModel("myuser@user.com");
            
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLiveCombatStatistics()
        {
            var viewModel = this.liveService.GetLiveStatisticsViewModel("myuser@user.com");

            return this.PartialView("_LiveStatisticsViewModel", viewModel);
        }
    }
}