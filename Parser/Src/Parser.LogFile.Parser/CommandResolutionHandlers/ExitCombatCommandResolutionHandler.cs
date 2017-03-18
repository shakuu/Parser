using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers.Base;
using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.CommandResolutionHandlers
{
    public class ExitCombatCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler, ICommandResolutionHandlerChain
    {
        private const string MatchingEventName = "ExitCombat";

        public ExitCombatCommandResolutionHandler()
            : base(ExitCombatCommandResolutionHandler.MatchingEventName)
        {

        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            combatStatisticsContainer.CurrentCombatStatistics.ExitCombatTime = command.TimeStamp;
            combatStatisticsContainer.CurrentCombatStatistics.IsCompleted = true;
            combatStatisticsContainer.CurrentCombatStatistics = null;

            return combatStatisticsContainer;
        }
    }
}
