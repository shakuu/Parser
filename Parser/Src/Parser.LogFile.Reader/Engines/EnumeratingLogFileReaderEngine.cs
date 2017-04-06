using System;

using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.Reader.Factories;

namespace Parser.LogFile.Reader.Engines
{
    public class EnumeratingLogFileReaderEngine : LogFileReaderEngine, ILogFileReaderEngine, IDisposable
    {
        private readonly IForceCommandUtilizationStrategy forceCommandUtilizationStrategy;

        public EnumeratingLogFileReaderEngine(ICommandParsingStrategy commandParsingStrategy, IForceCommandUtilizationStrategy forceCommandUtilizationStrategy, ILogFilePathDiscoveryStrategy logFilePathDiscoveryStrategy, IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory, IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory, IFileReaderInputProviderFactory fileReaderInputProviderFactory)
            : base(commandParsingStrategy, forceCommandUtilizationStrategy, logFilePathDiscoveryStrategy, fileReaderAutoResetEventFactory, fileReaderFileSystemWatcherFactory, fileReaderInputProviderFactory)
        {
            this.forceCommandUtilizationStrategy = forceCommandUtilizationStrategy;
        }

        protected override void AwaitLogFileChanges()
        {
            this.forceCommandUtilizationStrategy.ForceUtilizeCommand();

            base.AwaitLogFileChanges();
        }
    }
}
