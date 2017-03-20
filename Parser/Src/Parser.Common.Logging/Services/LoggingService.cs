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

        public void Log(string message, MessageType messageType)
        {
            Guard.WhenArgument(message, nameof(message)).IsNullOrEmpty().Throw();

            var logEntry = this.logEntryFactory.CreateLogEntry(message, messageType);

            this.loggingServicePersistentStorageStrategy.StoreLogEntry(logEntry);
        }
    }
}
