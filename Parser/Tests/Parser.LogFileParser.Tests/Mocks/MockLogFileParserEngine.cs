using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Engines;
using Parser.LogFileParser.Factories;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockLogFileParserEngine : LogFileParserEngine
    {
        internal MockLogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainer combatStatisticsContainer, IExitCombatEventArgsFactory exitCombatEventArgsFactory)
            : base(commandResolutionHandler, combatStatisticsContainer, exitCombatEventArgsFactory)
        {
        }

        internal new ICombatStatisticsContainer CombatStatisticsContainer { get { return base.CombatStatisticsContainer; } set { base.CombatStatisticsContainer = value; } }
    }
}
