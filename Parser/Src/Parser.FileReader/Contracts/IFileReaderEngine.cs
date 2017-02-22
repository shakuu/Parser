namespace Parser.FileReader.Contracts
{
    public interface IFileReaderEngine
    {
        void Start(string logFilePath);

        void Stop();
    }
}
