using System;
using System.Threading.Tasks;

namespace Parser.LogFile.Reader.Contracts
{
    public interface ILogFileReaderEngine : IDisposable
    {
        Task StartAsync();

        void Stop();
    }
}
