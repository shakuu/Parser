using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Factories
{
    public interface IFileReaderInputProviderFactory
    {
        IFileReaderInputProvider CreateFileReaderInputProvider(string filePath);
    }
}
