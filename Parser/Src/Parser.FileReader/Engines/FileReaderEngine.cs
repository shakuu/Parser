using Bytes2you.Validation;

using Parser.FileReader.Contracts;
using Parser.FileReader.Factories;

namespace Parser.FileReader.Engines
{
    public class FileReaderEngine : IFileReaderEngine
    {
        private readonly ICommandParsingStrategy commandParsingStrategy;
        private readonly ICommandUtilizationStrategy commandUtilizationStrategy;
        private readonly IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory;
        private readonly IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory;
        private readonly IFileReaderInputProviderFactory fileReaderInputProviderFactory;

        private bool isRunning = false;

        public FileReaderEngine(ICommandParsingStrategy commandParsingStrategy, ICommandUtilizationStrategy commandUtilizationStrategy, IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory, IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory, IFileReaderInputProviderFactory fileReaderInputProviderFactory)
        {
            Guard.WhenArgument(commandParsingStrategy, nameof(ICommandParsingStrategy)).IsNull().Throw();
            Guard.WhenArgument(commandUtilizationStrategy, nameof(ICommandUtilizationStrategy)).IsNull().Throw();
            Guard.WhenArgument(fileReaderAutoResetEventFactory, nameof(IFileReaderAutoResetEventFactory)).IsNull().Throw();
            Guard.WhenArgument(fileReaderFileSystemWatcherFactory, nameof(IFileReaderFileSystemWatcherFactory)).IsNull().Throw();
            Guard.WhenArgument(fileReaderInputProviderFactory, nameof(IFileReaderInputProviderFactory)).IsNull().Throw();

            this.commandParsingStrategy = commandParsingStrategy;
            this.commandUtilizationStrategy = commandUtilizationStrategy;
            this.fileReaderAutoResetEventFactory = fileReaderAutoResetEventFactory;
            this.fileReaderFileSystemWatcherFactory = fileReaderFileSystemWatcherFactory;
            this.fileReaderInputProviderFactory = fileReaderInputProviderFactory;
        }

        public void Start(string logFilePath)
        {
            Guard.WhenArgument(logFilePath, "Invalid log file path.").IsNullOrEmpty().Throw();

            this.isRunning = true;

            var autoResetEvent = this.fileReaderAutoResetEventFactory.CreateFileReaderAutoResetEvent(false);
            var fileSystemWatcher = this.fileReaderFileSystemWatcherFactory.CreateFileReaderFileSystemWatcher(logFilePath, true, autoResetEvent);

            using (var inputProvider = this.fileReaderInputProviderFactory.CreateFileReaderInputProvider(logFilePath))
            {
                var nextInputLine = string.Empty;
                while (this.isRunning)
                {
                    nextInputLine = inputProvider.ReadLine();
                    if (nextInputLine != null)
                    {
                        var nextParsedCommand = this.commandParsingStrategy.ParseInputCommand(nextInputLine);
                        this.commandUtilizationStrategy.UtilizeCommand(nextParsedCommand);
                    }
                    else
                    {
                        autoResetEvent.WaitOne(1000);
                    }
                }
            }
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
