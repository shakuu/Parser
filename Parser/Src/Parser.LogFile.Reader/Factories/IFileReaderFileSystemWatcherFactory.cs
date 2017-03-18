using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.Factories
{
    public interface IFileReaderFileSystemWatcherFactory
    {
        IFileReaderFileSystemWatcher CreateFileReaderFileSystemWatcher(string filter, bool enableRaisingEvents, IFileReaderAutoResetEvent autoResetEvent);
    }
}
