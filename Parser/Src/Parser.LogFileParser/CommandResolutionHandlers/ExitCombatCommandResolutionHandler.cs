using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
{
    public class ExitCombatCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler, ICommandResolutionHandlerChain
    {
        private const string ViableEventName = "ExitCombat";

        protected override bool CanHandleCommand(ICommand command)
        {
            if (string.IsNullOrEmpty(command.EventName))
            {
                return false;
            }
            else
            {
                return command.EventName == ExitCombatCommandResolutionHandler.ViableEventName;
            }
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            combatStatisticsContainer.CurrentComabtStatistics.ExitCombatTime = command.TimeStamp;
            combatStatisticsContainer.CurrentComabtStatistics.IsCompleted = true;
            combatStatisticsContainer.CurrentComabtStatistics = null;

            return combatStatisticsContainer;
        }
    }
}
