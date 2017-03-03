using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Engines;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockLogFileParserEngine : LogFileParserEngine
    {
        internal MockLogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainerFactory combatStatisticsContainerFactory)
            : base(commandResolutionHandler, combatStatisticsContainerFactory)
        {
        }

        internal new ICombatStatisticsContainer CombatStatisticsContainer { get { return base.CombatStatisticsContainer; } set { base.CombatStatisticsContainer = value; } }
    }
}
