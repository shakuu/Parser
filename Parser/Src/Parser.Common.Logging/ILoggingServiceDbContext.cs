using System.Data.Entity;
using System.Threading.Tasks;

using Parser.Common.Logging.Models;

namespace Parser.Common.Logging
{
    public interface ILoggingServiceDbContext
    {
        IDbSet<LogEntry> LogEntries { get; set; }

        Task<int> SaveChangesAsync();
    }
}
