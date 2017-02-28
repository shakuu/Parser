using System;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        public void EnqueueCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public IParseResult GetParseResult()
        {
            throw new NotImplementedException();
        }
    }
}
