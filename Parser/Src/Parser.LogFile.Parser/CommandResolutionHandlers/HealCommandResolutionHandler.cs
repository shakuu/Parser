using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers.Base;
using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.CommandResolutionHandlers
{
    public class HealCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler, ICommandResolutionHandlerChain
    {
        private const string MatchingEventName = "Heal";

        public HealCommandResolutionHandler()
            : base(HealCommandResolutionHandler.MatchingEventName)
        {
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            if (combatStatisticsContainer.CurrentCombatStatistics != null)
            {
                combatStatisticsContainer.CurrentCombatStatistics.HealingDone += command.EffectEffectiveAmount;

                combatStatisticsContainer.CurrentCombatStatistics.ExitCombatTime = command.TimeStamp;
            }

            return combatStatisticsContainer;
        }
    }
}
