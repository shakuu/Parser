using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Factories
{
    public interface IFileReaderFileSystemWatcherFactory
    {
        IFileReaderFileSystemWatcher CreateFileReaderFileSystemWatcher(string filter, bool enableRaisingEvents, IFileReaderAutoResetEvent autoResetEvent);
    }
}
