using System;
using System.Linq;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.SignalR.Contracts;

namespace Parser.LogFile.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineManager;
        private readonly ICommandEnumerationJsonConvertProvider commandJsonConvertProvider;

        public LogFileParserHubService(ILogFileParserEngineManager logFileParserEngineManager, ICommandEnumerationJsonConvertProvider commandJsonConvertProvider)
        {
            Guard.WhenArgument(logFileParserEngineManager, nameof(ILogFileParserEngineManager)).IsNull().Throw();
            Guard.WhenArgument(commandJsonConvertProvider, nameof(ICommandEnumerationJsonConvertProvider)).IsNull().Throw();

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

        public string SendCommandEnumeration(string engineId, string serializedCommandEnumeration)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();

            var deserializedCommandsEnumeration = this.commandJsonConvertProvider.DeserializeCommandEnumeration(serializedCommandEnumeration);
            if (deserializedCommandsEnumeration == null)
            {
                throw new ArgumentException(nameof(deserializedCommandsEnumeration));
            }

            this.logFileParserEngineManager.EnqueueCommandEnumerationToEngineWithId(engineId, deserializedCommandsEnumeration);

            return deserializedCommandsEnumeration?.Last().TimeStamp.ToShortTimeString();
        }
    }
}
