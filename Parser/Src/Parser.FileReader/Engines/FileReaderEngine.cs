using System.IO;
using System.Threading;

using Bytes2you.Validation;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Engines
{
    public class FileReaderEngine : IFileReaderEngine
    {
        private const string TestingFilePath = @"C:\Users\colley\OneDrive\swtor-parser\sample-logs\CombatLogs\combat_2017-02-21_21_05_53_596881.txt";

        private readonly ICommandParsingStrategy commandParsingStrategy;

        private bool isRunning = false;

        public FileReaderEngine(ICommandParsingStrategy commandParsingStrategy)
        {
            Guard.WhenArgument(commandParsingStrategy, nameof(ICommandParsingStrategy)).IsNull().Throw();

            this.commandParsingStrategy = commandParsingStrategy;
        }

        public void Start(string logFilePath)
        {
            Guard.WhenArgument(logFilePath, "Invalid log file path.").IsNullOrEmpty().Throw();

            this.isRunning = true;

            var autoResetEvent = new AutoResetEvent(false);
            var fileStreamWatcher = new FileSystemWatcher(".");
            
            fileStreamWatcher.Filter = logFilePath;
            fileStreamWatcher.EnableRaisingEvents = true;
            fileStreamWatcher.Changed += (s, e) => autoResetEvent.Set();

            using (var fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    var nextInputLine = string.Empty;
                    while (this.isRunning)
                    {
                        nextInputLine = sr.ReadLine();
                        if (nextInputLine != null)
                        {
                            var nextParsedCommand = this.commandParsingStrategy.ParseInputCommand(nextInputLine);
                        }
                        else
                        {
                            autoResetEvent.WaitOne(1000);
                        }
                    }
                }

                fs.Close();
            }
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
