using System.Collections.Generic;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Factories;
using Parser.LogFileParser.Managers;

namespace Parser.LogFileParser.Tests.Mock
{
    public class MockLogFileParserEngineManager : LogFileParserEngineManager
    {
        public MockLogFileParserEngineManager(IGuidStringProvider guidStringProvider, ILogFileParserEngineFactory logFileParserEngineFactory)
            : base(guidStringProvider, logFileParserEngineFactory)
        {
        }

        public new IDictionary<string, ILogFileParserEngine> LogFileParserEngines { get { return base.LogFileParserEngines; } }
    }
}
