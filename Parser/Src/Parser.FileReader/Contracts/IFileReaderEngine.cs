namespace Parser.FileReader.Contracts
{
    public interface IFileReaderEngine
    {
        void StartAsync();

        void Start();

        void Stop();
    }
}
