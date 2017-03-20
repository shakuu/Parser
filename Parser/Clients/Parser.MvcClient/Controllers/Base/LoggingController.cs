using System;
using System.Web.Mvc;
using System.Web.Routing;

using Ninject;

using Parser.Common.Contracts;
using Parser.Common.Logging;
using Parser.MvcClient.App_Start;

namespace Parser.MvcClient.Controllers.Base
{
    public class LoggingController : Controller
    {
        private const string BeginExecutionMessage = "Begin execution";

        private readonly ILoggingService loggingService;
        private readonly IDateTimeProvider dateTimeProvider;

        public LoggingController()
        {
            // Do not want to contaminate inheriting controllers' constructors.
            this.loggingService = NinjectWebCommon.Kernel.Get<ILoggingService>();
            this.dateTimeProvider = NinjectWebCommon.Kernel.Get<IDateTimeProvider>();
        }

        private string Controller { get; set; }

        private string Action { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.Controller = (string)requestContext.RouteData.Values["controller"];
            this.Action = (string)requestContext.RouteData.Values["action"];

            this.loggingService.Log(this.Controller, this.Action, LoggingController.BeginExecutionMessage, MessageType.Info, this.dateTimeProvider.GetUtcNow());

            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            this.loggingService.Log(this.Controller, this.Action, filterContext.Exception.Message, MessageType.Error, this.dateTimeProvider.GetUtcNow());

            base.OnException(filterContext);
        }
    }
}