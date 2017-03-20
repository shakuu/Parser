using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Parser.MvcClient.Controllers.Base
{
    public class LoggingController : Controller
    {
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var controller = requestContext.RouteData.Values["controller"];
            var action = requestContext.RouteData.Values["action"];

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}