using System;

namespace Parser.Common.Logging
{
    public interface ILogEntry
    {
        string Message { get; set; }

        MessageType MessageType { get; set; }

        DateTime Timestamp { get; set; }

        string Action { get; set; }

        string Controller { get; set; }
    }
}
