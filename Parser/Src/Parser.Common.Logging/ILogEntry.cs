namespace Parser.Common.Logging
{
    public interface ILogEntry
    {
        string Message { get; set; }

        MessageType MessageType { get; set; }
    }
}
