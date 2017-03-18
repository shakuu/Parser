using System.Linq;

using Bytes2you.Validation;
using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.Strategies
{
    public class LogFilePathDiscoveryStrategy : ILogFilePathDiscoveryStrategy
    {
        private const string MyDocumentsFolderName = "MyDocuments";
        private const string DefaultCombatLogsPath = "\\Star Wars - The Old Republic\\CombatLogs";

        private readonly IEnvironmentFolderPathProvider environmentFolderPathProvider;
        private readonly IDirectoryFilesProvider directoryFilesProvider;

        public LogFilePathDiscoveryStrategy(IEnvironmentFolderPathProvider environmentFolderPathProvider, IDirectoryFilesProvider directoryFilesProvider)
        {
            Guard.WhenArgument(environmentFolderPathProvider, nameof(IEnvironmentFolderPathProvider)).IsNull().Throw();
            Guard.WhenArgument(directoryFilesProvider, nameof(IDirectoryFilesProvider)).IsNull().Throw();

            this.environmentFolderPathProvider = environmentFolderPathProvider;
            this.directoryFilesProvider = directoryFilesProvider;
        }

        public string DiscoverLogFile()
        {
            var documentsFolderPath = this.environmentFolderPathProvider.GetEnvironmentFolderPath(LogFilePathDiscoveryStrategy.MyDocumentsFolderName);
            var combatLogsPath = (documentsFolderPath ?? string.Empty) + LogFilePathDiscoveryStrategy.DefaultCombatLogsPath;
            var allLogFiles = this.directoryFilesProvider.GetDirectoryFiles(combatLogsPath);

            return allLogFiles?.LastOrDefault();
        }
    }
}
