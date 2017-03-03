using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineManager;
        private readonly ICommandJsonConvertProvider commandJsonConvertProvider;

        public LogFileParserHubService(ILogFileParserEngineManager logFileParserEngineManager, ICommandJsonConvertProvider commandJsonConvertProvider)
        {
            Guard.WhenArgument(logFileParserEngineManager, nameof(ILogFileParserEngineManager)).IsNull().Throw();
            Guard.WhenArgument(commandJsonConvertProvider, nameof(ICommandJsonConvertProvider)).IsNull().Throw();

            this.logFileParserEngineManager = logFileParserEngineManager;
            this.commandJsonConvertProvider = commandJsonConvertProvider;
        }

        public string GetParsingSessionId()
        {
            return this.logFileParserEngineManager.StartNewLogFileParserEngine();
        }

        public string SendCommand(string engineId, string serializedCommand)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();

            var command = this.commandJsonConvertProvider.DeserializeCommand(serializedCommand);
            if (command == null)
            {
                throw new ArgumentException(nameof(serializedCommand));
            }

            this.logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command);

            return command.TimeStamp.ToShortTimeString();
        }
    }
}
