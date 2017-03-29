using System.Data.Entity;

using Parser.Common.Constants.Configuration;
using Parser.Common.Logging.Models;

namespace Parser.Common.Logging
{
    public class LoggingServiceDbContext : DbContext, ILoggingServiceDbContext
    {
        public LoggingServiceDbContext()
            : base($"name={ConnectionStrings.ParserDbConnectionString}")
        {

        }

        public virtual IDbSet<LogEntry> LogEntries { get; set; }
    }
}
