using System.Threading.Tasks;

namespace Parser.LogFileReader.Contracts
{
    public interface IFileReaderEngine
    {
        Task StartAsync();

        void Stop();
    }
}
