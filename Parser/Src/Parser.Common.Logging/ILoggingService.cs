using System;

namespace Parser.Common.Logging
{
    public interface ILoggingService
    {
        void Log(string controller, string method, string message, MessageType messageType, DateTime timestamp);
    }
}
