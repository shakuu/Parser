using System.Web.Mvc;

namespace Parser.MvcClient.Controllers
{
    public class AdminFabController : Controller
    {
        [HttpGet]
        public ActionResult AdminFab()
        {
            return this.PartialView();
        }
    }
}