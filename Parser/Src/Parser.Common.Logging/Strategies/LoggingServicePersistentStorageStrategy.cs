using Bytes2you.Validation;

using Parser.Common.Logging.Models;

namespace Parser.Common.Logging.Strategies
{
    public class LoggingServicePersistentStorageStrategy : ILoggingServicePersistentStorageStrategy
    {
        private readonly ILoggingServiceDbContext loggingServiceDbContext;

        public LoggingServicePersistentStorageStrategy(ILoggingServiceDbContext loggingServiceDbContext)
        {
            Guard.WhenArgument(loggingServiceDbContext, nameof(ILoggingServiceDbContext)).IsNull().Throw();

            this.loggingServiceDbContext = loggingServiceDbContext;
        }

        public async void StoreLogEntry(LogEntry logEntry)
        {
            Guard.WhenArgument(logEntry, nameof(ILogEntry)).IsNull().Throw();

            this.loggingServiceDbContext.LogEntries.Add(logEntry);

            await this.loggingServiceDbContext.SaveChangesAsync();
        }
    }
}
