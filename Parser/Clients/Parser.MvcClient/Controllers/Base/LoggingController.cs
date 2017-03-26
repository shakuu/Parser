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

        private ILoggingService loggingService;
        private IDateTimeProvider dateTimeProvider;

        private ILoggingService LoggingService
        {
            get
            {
                if (this.loggingService == null)
                {
                    this.loggingService = NinjectWebCommon.Kernel.Get<ILoggingService>();
                }

                return this.loggingService;
            }
        }

        private IDateTimeProvider DateTimeProvider
        {
            get
            {
                if (this.dateTimeProvider == null)
                {
                    this.dateTimeProvider = NinjectWebCommon.Kernel.Get<IDateTimeProvider>();
                }

                return this.dateTimeProvider;
            }
        }

        private string Controller { get; set; }

        private string Action { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.Controller = (string)requestContext.RouteData.Values["controller"];
            this.Action = (string)requestContext.RouteData.Values["action"];

            this.LoggingService.Log(this.Controller, this.Action, LoggingController.BeginExecutionMessage, MessageType.Info, this.DateTimeProvider.GetUtcNow());

            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            this.LoggingService.Log(this.Controller, this.Action, filterContext.Exception.Message, MessageType.Error, this.DateTimeProvider.GetUtcNow());

            base.OnException(filterContext);
        }
    }
}