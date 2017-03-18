using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Factories
{
    public interface IFileReaderAutoResetEventFactory
    {
        IFileReaderAutoResetEvent CreateFileReaderAutoResetEvent(bool initialState);
    }
}
