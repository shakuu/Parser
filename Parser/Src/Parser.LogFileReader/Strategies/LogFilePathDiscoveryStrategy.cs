using System.Linq;

using Bytes2you.Validation;

using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Strategies
{
    public class LogFilePathDiscoveryStrategy : ILogFilePathDiscoveryStrategy
    {
        private const string MyDocumentsName = "MyDocuments";
        private const string SwtorDefaultPath = "\\Star Wars - The Old Republic\\CombatLogs";

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
            var documentsFolderPath = this.environmentFolderPathProvider.GetEnvironmentFolderPath(LogFilePathDiscoveryStrategy.MyDocumentsName);
            var combatLogsPath = documentsFolderPath + LogFilePathDiscoveryStrategy.SwtorDefaultPath;
            var allLogFiles = this.directoryFilesProvider.GetDirectoryFiles(combatLogsPath);

            return allLogFiles.Last();
        }
    }
}
