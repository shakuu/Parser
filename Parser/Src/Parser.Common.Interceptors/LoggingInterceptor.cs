using System;

using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Logging;

namespace Parser.Common.Interceptors
{
    public class LoggingInterceptor : IInterceptor
    {
        private const string BeginExecutionMessage = "Begin execution";
        private const string EndExecutionMessage = "End execution";

        private readonly ILoggingService loggingService;
        private readonly IDateTimeProvider dateTimeProvider;

        public LoggingInterceptor(ILoggingService loggingService, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(loggingService, nameof(ILoggingService)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.loggingService = loggingService;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            var controller = invocation.Request.Target.GetType().Name;
            var method = invocation.Request.Method.Name;

            try
            {
                this.loggingService.Log(controller, method, LoggingInterceptor.BeginExecutionMessage, MessageType.Info, this.dateTimeProvider.GetUtcNow());
                invocation.Proceed();
                this.loggingService.Log(controller, method, LoggingInterceptor.EndExecutionMessage, MessageType.Info, this.dateTimeProvider.GetUtcNow());
            }
            catch (Exception e)
            {
                this.loggingService.Log(controller, method, e.Message, MessageType.Error, this.dateTimeProvider.GetUtcNow());
            }
        }
    }
}
