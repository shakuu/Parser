using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
{
    public class DamageCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler, ICommandResolutionHandlerChain
    {
        private const string MatchingEventName = "Damage";

        public DamageCommandResolutionHandler()
            : base(DamageCommandResolutionHandler.MatchingEventName)
        {
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            if (combatStatisticsContainer.CurrentCombatStatistics != null)
            {
                combatStatisticsContainer.CurrentCombatStatistics.DamageDone += command.EffectEffectiveAmount;
            }

            return combatStatisticsContainer;
        }
    }
}
