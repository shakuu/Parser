using System.Collections.Generic;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Factories;
using Parser.LogFile.Parser.Managers;

namespace Parser.LogFile.Parser.Tests.Mocks
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
