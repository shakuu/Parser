using System;
using System.Web;

using Ninject;

using Parser.Common.Contracts;
using Parser.Common.Logging;
using Parser.MvcClient.App_Start;

namespace Parser.MvcClient.HttpModules
{
    public class LoggingModule : IHttpModule
    {
        private const string BeginExecutionMessage = "BeginRequest";
        private const string ErrorMessage = "Error";

        private readonly ILoggingService loggingService;
        private readonly IDateTimeProvider dateTimeProvider;

        public LoggingModule()
        {
            this.loggingService = NinjectWebCommon.Kernel.Get<ILoggingService>();
            this.dateTimeProvider = NinjectWebCommon.Kernel.Get<IDateTimeProvider>();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.OnBeginRequest;
            context.Error += this.OnError;
        }

        private string Controller { get; set; }

        private string Action { get; set; }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            this.Controller = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
            this.Action = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["action"];

            this.loggingService.Log(this.Controller, this.Action, LoggingModule.BeginExecutionMessage, MessageType.Info, this.dateTimeProvider.GetUtcNow());
        }

        private void OnError(object sender, EventArgs e)
        {
            this.loggingService.Log(this.Controller, this.Action, LoggingModule.ErrorMessage, MessageType.Error, this.dateTimeProvider.GetUtcNow());
        }

        public void Dispose()
        {
        }
    }
}