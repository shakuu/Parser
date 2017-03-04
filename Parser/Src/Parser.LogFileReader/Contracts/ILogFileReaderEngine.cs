using System;
using System.Threading.Tasks;

namespace Parser.LogFileReader.Contracts
{
    public interface ILogFileReaderEngine : IDisposable
    {
        Task StartAsync();

        void Stop();
    }
}
