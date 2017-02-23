using Parser.FileReader.Contracts;

namespace Parser.FileReader.Factories
{
    public interface IFileReaderAutoResetEventFactory
    {
        IFileReaderAutoResetEvent CreateFileReaderAutoResetEvent(bool initialState);
    }
}
