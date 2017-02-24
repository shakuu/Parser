namespace Parser.FileReader.Contracts
{
    public interface IFileReaderEngine
    {
        void StartAsync(string logFilePath);

        void Start(string logFilePath);

        void Stop();
    }
}
