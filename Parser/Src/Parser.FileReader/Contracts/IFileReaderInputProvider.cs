using System;

namespace Parser.FileReader.Contracts
{
    public interface IFileReaderInputProvider : IDisposable
    {
        string ReadLine();
    }
}
