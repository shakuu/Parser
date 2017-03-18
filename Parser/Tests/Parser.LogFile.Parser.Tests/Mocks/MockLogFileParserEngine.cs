using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Engines;

namespace Parser.LogFile.Parser.Tests.Mocks
{
    internal class MockLogFileParserEngine : LogFileParserEngine
    {
        internal MockLogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainer combatStatisticsContainer, ICombatStatisticsFinalizationStrategy combatStatisticsFinalizationStrategy, ICombatStatisticsPersistentStorageStrategy combatStatisticsPersistentStorageStrategy)
            : base(commandResolutionHandler, combatStatisticsContainer, combatStatisticsFinalizationStrategy, combatStatisticsPersistentStorageStrategy)
        {
        }

        internal new ICombatStatisticsContainer CombatStatisticsContainer { get { return base.CombatStatisticsContainer; } set { base.CombatStatisticsContainer = value; } }

        internal new void OnCurrentCombatStatisticsChanged(object sender, CurrentCombatStatisticsChangedEventArgs args)
        {
            base.OnCurrentCombatStatisticsChanged(sender, args);
        }
    }
}
