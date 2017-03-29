using System;
using System.Threading.Tasks;

using Bytes2you.Validation;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.Reader.Factories;

namespace Parser.LogFile.Reader.Engines
{
    public class LogFileReaderEngine : ILogFileReaderEngine, IDisposable
    {
        private const int AutoResetEventWaitTimeoutInMiliseconds = 250;

        private readonly ICommandParsingStrategy commandParsingStrategy;
        private readonly ICommandUtilizationStrategy commandUtilizationStrategy;
        private readonly ILogFilePathDiscoveryStrategy logFilePathDiscoveryStrategy;
        private readonly IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory;
        private readonly IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory;
        private readonly IFileReaderInputProviderFactory fileReaderInputProviderFactory;

        private bool isRunning = false;

        public LogFileReaderEngine(ICommandParsingStrategy commandParsingStrategy, ICommandUtilizationStrategy commandUtilizationStrategy, ILogFilePathDiscoveryStrategy logFilePathDiscoveryStrategy, IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory, IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory, IFileReaderInputProviderFactory fileReaderInputProviderFactory)
        {
            Guard.WhenArgument(commandParsingStrategy, nameof(ICommandParsingStrategy)).IsNull().Throw();
            Guard.WhenArgument(commandUtilizationStrategy, nameof(ICommandUtilizationStrategy)).IsNull().Throw();
            Guard.WhenArgument(logFilePathDiscoveryStrategy, nameof(ILogFilePathDiscoveryStrategy)).IsNull().Throw();
            Guard.WhenArgument(fileReaderAutoResetEventFactory, nameof(IFileReaderAutoResetEventFactory)).IsNull().Throw();
            Guard.WhenArgument(fileReaderFileSystemWatcherFactory, nameof(IFileReaderFileSystemWatcherFactory)).IsNull().Throw();
            Guard.WhenArgument(fileReaderInputProviderFactory, nameof(IFileReaderInputProviderFactory)).IsNull().Throw();

            this.commandParsingStrategy = commandParsingStrategy;
            this.commandUtilizationStrategy = commandUtilizationStrategy;
            this.logFilePathDiscoveryStrategy = logFilePathDiscoveryStrategy;
            this.fileReaderAutoResetEventFactory = fileReaderAutoResetEventFactory;
            this.fileReaderFileSystemWatcherFactory = fileReaderFileSystemWatcherFactory;
            this.fileReaderInputProviderFactory = fileReaderInputProviderFactory;
        }

        public Task StartAsync()
        {
            return Task.Run(() => this.Run());
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        private void Run()
        {
            this.isRunning = true;

            var logFilePath = this.logFilePathDiscoveryStrategy.DiscoverLogFile();

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
                        var nextParsedCommand = this.commandParsingStrategy.ParseCommand(nextInputLine);
                        if (nextParsedCommand != null)
                        {
                            this.commandUtilizationStrategy.UtilizeCommand(nextParsedCommand);
                        }
                    }
                    else
                    {
                        autoResetEvent.WaitOne(LogFileReaderEngine.AutoResetEventWaitTimeoutInMiliseconds);
                    }
                }
            }
        }

        public void Dispose()
        {
            this.commandUtilizationStrategy.Dispose();
        }
    }
}
