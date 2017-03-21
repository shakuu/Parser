using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        [ChildActionOnly]
        public ActionResult DisplayAdminFab()
        {
            return this.PartialView("_AdminFabPartial");
        }
    }
}