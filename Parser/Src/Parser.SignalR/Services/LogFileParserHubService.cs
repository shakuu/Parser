using Bytes2you.Validation;

using Parser.Common.Models;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineService;
        private readonly IJsonConvertProvider jsonConvertProvider;

        public LogFileParserHubService(ILogFileParserEngineManager logFileParserEngineService, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(logFileParserEngineService, nameof(ILogFileParserEngineManager)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.logFileParserEngineService = logFileParserEngineService;
            this.jsonConvertProvider = jsonConvertProvider;
        }

        public string GetParsingSessionId()
        {
            return this.logFileParserEngineService.StartNewLogFileParserEngine();
        }

        public string SendCommand(string engineId, string serializedCommand)
        {
            var command = this.jsonConvertProvider.DeserializeObject<Command>(serializedCommand);

            return this.logFileParserEngineService.EnqueueCommandToEngineWithId(engineId, command);
        }
    }
}
