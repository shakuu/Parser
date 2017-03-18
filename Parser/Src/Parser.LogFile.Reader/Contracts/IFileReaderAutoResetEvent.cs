namespace Parser.LogFile.Reader.Contracts
{
    public interface IFileReaderAutoResetEvent
    {
        bool Set();

        bool WaitOne(int milisecondsTimeout);
    }
}
