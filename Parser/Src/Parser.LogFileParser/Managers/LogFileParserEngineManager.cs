using System;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Managers
{
    public class LogFileParserEngineManager : ILogFileParserEngineManager
    {
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
