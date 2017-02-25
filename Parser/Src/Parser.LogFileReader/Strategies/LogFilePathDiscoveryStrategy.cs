using System.IO;
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

        public LogFilePathDiscoveryStrategy(IEnvironmentFolderPathProvider environmentFolderPathProvider)
        {
            Guard.WhenArgument(environmentFolderPathProvider, nameof(IEnvironmentFolderPathProvider)).IsNull().Throw();

            this.environmentFolderPathProvider = environmentFolderPathProvider;
        }

        public string DiscoverLogFile()
        {
            var documentsFolderPath = this.environmentFolderPathProvider.GetEnvironmentFolderPath(LogFilePathDiscoveryStrategy.MyDocumentsName);
            var combatLogsPath = documentsFolderPath + LogFilePathDiscoveryStrategy.SwtorDefaultPath;
            var logFilePath = Directory.GetFiles(combatLogsPath);

            return logFilePath.Last();
        }
    }
}
