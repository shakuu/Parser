using System.Collections.Generic;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Factories;
using Parser.LogFileParser.Managers;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockLogFileParserEngineManager : LogFileParserEngineManager
    {
        internal MockLogFileParserEngineManager(IGuidStringProvider guidStringProvider, ILogFileParserEngineFactory logFileParserEngineFactory)
            : base(guidStringProvider, logFileParserEngineFactory)
        {
        }

        internal new IDictionary<string, ILogFileParserEngine> LogFileParserEngines { get { return base.LogFileParserEngines; } }
    }
}
