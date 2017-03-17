using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
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
            }

            return combatStatisticsContainer;
        }
    }
}
