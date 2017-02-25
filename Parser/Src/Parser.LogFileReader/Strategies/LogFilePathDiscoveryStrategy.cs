using System;
using System.IO;
using System.Linq;

using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Strategies
{
    public class LogFilePathDiscoveryStrategy : ILogFilePathDiscoveryStrategy
    {
        private const string SwtorDefaultPath = "\\Star Wars - The Old Republic\\CombatLogs";

        public string DiscoverLogFile()
        {
            var documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var combatLogsPath = documentsFolderPath + LogFilePathDiscoveryStrategy.SwtorDefaultPath;
            var logFilePath = Directory.GetFiles(combatLogsPath);

            return logFilePath.Last();
        }
    }
}
