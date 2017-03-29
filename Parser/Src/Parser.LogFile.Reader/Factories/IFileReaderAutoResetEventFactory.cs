using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.Factories
{
    public interface IFileReaderAutoResetEventFactory
    {
        IFileReaderAutoResetEvent CreateFileReaderAutoResetEvent(bool initialState);
    }
}
