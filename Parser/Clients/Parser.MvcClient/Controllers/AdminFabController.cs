using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        [ChildActionOnly]
        public ActionResult AdminFab()
        {
            return this.PartialView();
        }
    }
}