namespace Parser.Common.Logging
{
    public interface ILoggingServicePersistentStorageStrategy
    {
        void StoreLogEntry(ILogEntry logEntry);
    }
}
