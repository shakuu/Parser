using System.IO;

using Bytes2you.Validation;
using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.FileSystemProviders
{
    public class FileReaderFileSystemWatcher : IFileReaderFileSystemWatcher
    {
        private readonly FileSystemWatcher fileSystemWatcher;

        public FileReaderFileSystemWatcher(string filter, bool enableRaisingEvents, IFileReaderAutoResetEvent autoResetEvent)
        {
            Guard.WhenArgument(filter, nameof(filter)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(autoResetEvent, nameof(IFileReaderAutoResetEvent)).IsNull().Throw();

            this.fileSystemWatcher = new FileSystemWatcher(".");

            this.fileSystemWatcher.Filter = filter;
            this.fileSystemWatcher.EnableRaisingEvents = enableRaisingEvents;
            this.fileSystemWatcher.Changed += this.CreateOnChangedEventHandler(autoResetEvent);
        }

        private FileSystemEventHandler CreateOnChangedEventHandler(IFileReaderAutoResetEvent autoResetEvent)
        {
            return (sender, args) => autoResetEvent.Set();
        }
    }
}
