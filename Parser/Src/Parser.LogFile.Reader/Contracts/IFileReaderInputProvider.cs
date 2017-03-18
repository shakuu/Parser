using System;

namespace Parser.LogFile.Reader.Contracts
{
    public interface IFileReaderInputProvider : IDisposable
    {
        string ReadLine();
    }
}
