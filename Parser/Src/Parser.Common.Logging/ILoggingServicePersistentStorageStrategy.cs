using Parser.Common.Logging.Models;

namespace Parser.Common.Logging
{
    public interface ILoggingServicePersistentStorageStrategy
    {
        void StoreLogEntry(LogEntry logEntry);
    }
}
