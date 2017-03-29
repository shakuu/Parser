using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.SignalR.Contracts;

namespace Parser.LogFile.SignalR.Services
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

        public string GetParsingSessionId(string username)
        {
            return this.logFileParserEngineManager.StartLogFileParserEngine(username);
        }

        public string ReleaseParsingSessionId(string engineId)
        {
            return this.logFileParserEngineManager.StopLogFileParserEngine(engineId);
        }

        public string SendCommand(string engineId, string serializedCommand)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();

            var deserializedCommand = this.commandJsonConvertProvider.DeserializeCommand(serializedCommand);
            if (deserializedCommand == null)
            {
                throw new ArgumentException(nameof(serializedCommand));
            }

            this.logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, deserializedCommand);

            // TODO: Remove? Replace with something meaningful?
            return deserializedCommand.TimeStamp.ToShortTimeString();
        }
    }
}
