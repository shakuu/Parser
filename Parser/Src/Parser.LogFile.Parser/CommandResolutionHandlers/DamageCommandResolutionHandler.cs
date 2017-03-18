using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers.Base;
using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.CommandResolutionHandlers
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
