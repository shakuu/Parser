using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Engines;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockLogFileParserEngine : LogFileParserEngine
    {
        internal MockLogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainer combatStatisticsContainer)
            : base(commandResolutionHandler, combatStatisticsContainer)
        {
        }

        internal new ICombatStatisticsContainer CombatStatisticsContainer { get { return base.CombatStatisticsContainer; } set { base.CombatStatisticsContainer = value; } }
    }
}
