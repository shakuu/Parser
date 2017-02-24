using System;

namespace Parser.LogFileReader.Contracts
{
    public interface IFileReaderInputProvider : IDisposable
    {
        string ReadLine();
    }
}
