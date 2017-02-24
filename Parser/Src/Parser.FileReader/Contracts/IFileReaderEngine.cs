using System.Threading.Tasks;

namespace Parser.FileReader.Contracts
{
    public interface IFileReaderEngine
    {
        Task StartAsync();

        void Stop();
    }
}
