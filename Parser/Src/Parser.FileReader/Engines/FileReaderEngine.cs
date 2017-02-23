using System.IO;
using System.Threading;

using Bytes2you.Validation;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Engines
{
    public class FileReaderEngine : IFileReaderEngine
    {
        private readonly ICommandParsingStrategy commandParsingStrategy;
        private readonly ICommandUtilizationStrategy commandUtilizationStrategy;

        private bool isRunning = false;

        public FileReaderEngine(ICommandParsingStrategy commandParsingStrategy, ICommandUtilizationStrategy commandUtilizationStrategy)
        {
            Guard.WhenArgument(commandParsingStrategy, nameof(ICommandParsingStrategy)).IsNull().Throw();
            Guard.WhenArgument(commandUtilizationStrategy, nameof(ICommandUtilizationStrategy)).IsNull().Throw();

            this.commandParsingStrategy = commandParsingStrategy;
            this.commandUtilizationStrategy = commandUtilizationStrategy;
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
                            this.commandUtilizationStrategy.UtilizeCommand(nextParsedCommand);
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
