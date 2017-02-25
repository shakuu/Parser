using System.Threading.Tasks;

namespace Parser.LogFileReader.Contracts
{
    public interface ILogFileReaderEngine
    {
        Task StartAsync();

        void Stop();
    }
}
