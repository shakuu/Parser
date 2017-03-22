using System.Collections.Generic;

using Parser.Common.Logging.Models;

namespace Parser.Common.Logging
{
    public interface ILogReportingDataProvider
    {
        IEnumerable<LogEntry> GetErrorsForPeriod(int periodInHours);
    }
}
