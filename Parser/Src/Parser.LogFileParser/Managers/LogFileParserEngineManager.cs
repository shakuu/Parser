using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Managers
{
    public class LogFileParserEngineManager : ILogFileParserEngineManager
    {
        private readonly IGuidStringProvider guidProvider;

        private readonly IDictionary<string, ILogFileParserEngine> logFileParserEngines;

        public LogFileParserEngineManager(IGuidStringProvider guidProvider)
        {
            Guard.WhenArgument(guidProvider, nameof(IGuidStringProvider)).IsNull().Throw();

            this.guidProvider = guidProvider;
        }

        public string EnqueueCommandToEngineWithId(string engineId, ICommand command)
        {
            throw new NotImplementedException();
        }

        public string StartNewLogFileParserEngine()
        {
            throw new NotImplementedException();
        }
    }
}
