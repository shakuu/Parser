using Parser.Common.Logging.Models;

namespace Parser.Common.Logging.Factories
{
    public interface ILogEntryFactory
    {
        LogEntry CreateLogEntry(string message, MessageType messageType);
    }
}
