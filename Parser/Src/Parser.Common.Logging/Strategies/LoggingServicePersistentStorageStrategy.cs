using Bytes2you.Validation;

using Parser.Common.Logging.Factories;

namespace Parser.Common.Logging.Strategies
{
    public class LoggingServicePersistentStorageStrategy : ILoggingServicePersistentStorageStrategy
    {
        private readonly ILoggingServiceDbContext loggingServiceDbContext;
        private readonly ILogEntryFactory logEntryFactory;

        public LoggingServicePersistentStorageStrategy(ILoggingServiceDbContext loggingServiceDbContext, ILogEntryFactory logEntryFactory)
        {
            Guard.WhenArgument(loggingServiceDbContext, nameof(ILoggingServiceDbContext)).IsNull().Throw();
            Guard.WhenArgument(logEntryFactory, nameof(ILogEntryFactory)).IsNull().Throw();

            this.loggingServiceDbContext = loggingServiceDbContext;
            this.logEntryFactory = logEntryFactory;
        }

        public async void StoreLogEntry(ILogEntry logEntry)
        {
            var entry = this.logEntryFactory.CreateLogEntry(logEntry);

            this.loggingServiceDbContext.LogEntries.Add(entry);

            await this.loggingServiceDbContext.SaveChangesAsync();
        }
    }
}
