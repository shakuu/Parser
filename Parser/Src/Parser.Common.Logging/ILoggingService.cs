namespace Parser.Common.Logging
{
    public interface ILoggingService
    {
        void Log(string message, MessageType messageType);
    }
}
