using Parser.FileReader.Contracts;

namespace Parser.FileReader.Factories
{
    public interface IFileReaderInputProviderFactory
    {
        IFileReaderInputProvider CreateFileReaderInputProvider(string filePath);
    }
}
