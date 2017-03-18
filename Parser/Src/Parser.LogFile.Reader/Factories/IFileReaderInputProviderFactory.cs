using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.Factories
{
    public interface IFileReaderInputProviderFactory
    {
        IFileReaderInputProvider CreateFileReaderInputProvider(string filePath);
    }
}
