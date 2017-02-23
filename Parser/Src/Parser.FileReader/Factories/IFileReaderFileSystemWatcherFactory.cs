using Parser.FileReader.Contracts;

namespace Parser.FileReader.Factories
{
    public interface IFileReaderFileSystemWatcherFactory
    {
        IFileReaderFileSystemWatcher CreateFileReaderFileSystemWatcher(string filter, bool enableRaisingEvents, IFileReaderAutoResetEvent autoResetEvent);
    }
}
