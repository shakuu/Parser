using System;

using Bytes2you.Validation;

using Parser.Common.Logging.Factories;

namespace Parser.Common.Logging.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingServicePersistentStorageStrategy loggingServicePersistentStorageStrategy;
        private readonly ILogEntryFactory logEntryFactory;

        public LoggingService(ILoggingServicePersistentStorageStrategy loggingServicePersistentStorageStrategy, ILogEntryFactory logEntryFactory)
        {
            Guard.WhenArgument(loggingServicePersistentStorageStrategy, nameof(ILoggingServicePersistentStorageStrategy)).IsNull().Throw();
            Guard.WhenArgument(logEntryFactory, nameof(ILogEntryFactory)).IsNull().Throw();

            this.loggingServicePersistentStorageStrategy = loggingServicePersistentStorageStrategy;
            this.logEntryFactory = logEntryFactory;
        }

        public void Log(string controller, string method, string message, MessageType messageType, DateTime timestamp)
        {
            Guard.WhenArgument(message, nameof(message)).IsNullOrEmpty().Throw();

            var logEntry = this.logEntryFactory.CreateLogEntry();
            logEntry.Controller = controller;
            logEntry.Action = method;
            logEntry.Message = message;
            logEntry.MessageType = messageType;
            logEntry.Timestamp = timestamp;

            this.loggingServicePersistentStorageStrategy.StoreLogEntry(logEntry);
        }
    }
}
