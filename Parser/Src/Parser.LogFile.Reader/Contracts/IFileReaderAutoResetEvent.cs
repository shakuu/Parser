namespace Parser.LogFileReader.Contracts
{
    public interface IFileReaderAutoResetEvent
    {
        bool Set();

        bool WaitOne(int milisecondsTimeout);
    }
}
