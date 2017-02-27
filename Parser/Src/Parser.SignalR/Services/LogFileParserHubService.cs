using System;

using Bytes2you.Validation;

using Parser.LogFileReader.Models;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        private readonly IJsonConvertProvider jsonConvertProvider;

        public LogFileParserHubService(IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.jsonConvertProvider = jsonConvertProvider;
        }

        public string GetParsingSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        public string SendCommand(string userId, string serializedCommand)
        {
            var command = this.jsonConvertProvider.DeserializeObject<Command>(serializedCommand);

            return command.TimeStamp.ToShortTimeString();
        }
    }
}
