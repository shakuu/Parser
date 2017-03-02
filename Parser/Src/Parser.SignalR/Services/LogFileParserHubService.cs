using Bytes2you.Validation;

using Parser.Common.Models;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineManager;
        private readonly IJsonConvertProvider jsonConvertProvider;

        public LogFileParserHubService(ILogFileParserEngineManager logFileParserEngineManager, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(logFileParserEngineManager, nameof(ILogFileParserEngineManager)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.logFileParserEngineManager = logFileParserEngineManager;
            this.jsonConvertProvider = jsonConvertProvider;
        }

        public string GetParsingSessionId()
        {
            return this.logFileParserEngineManager.StartNewLogFileParserEngine();
        }

        public string SendCommand(string engineId, string serializedCommand)
        {
            var command = this.jsonConvertProvider.DeserializeObject<Command>(serializedCommand);

            this.logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command);

            return command.TimeStamp.ToShortTimeString();
        }
    }
}
