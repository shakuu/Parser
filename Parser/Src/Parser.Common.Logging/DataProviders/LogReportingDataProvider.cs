using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Logging.Models;

namespace Parser.Common.Logging.DataProviders
{
    public class LogReportingDataProvider : ILogReportingDataProvider
    {
        private readonly ILoggingServiceDbContext loggingServiceDbContext;
        private readonly IDateTimeProvider dateTimeProvider;

        public LogReportingDataProvider(ILoggingServiceDbContext loggingServiceDbContext, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(loggingServiceDbContext, nameof(ILoggingServiceDbContext)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.loggingServiceDbContext = loggingServiceDbContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<LogEntry> GetErrorsForPeriod(int periodInHours)
        {
            if (periodInHours < 1)
            {
                periodInHours = 1;
            }

            var timestampConstraint = this.dateTimeProvider.GetUtcNow().AddHours(-periodInHours);
            return this.loggingServiceDbContext.LogEntries
                .Where(l => l.Timestamp > timestampConstraint && l.MessageType == MessageType.Error)
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }
    }
}
