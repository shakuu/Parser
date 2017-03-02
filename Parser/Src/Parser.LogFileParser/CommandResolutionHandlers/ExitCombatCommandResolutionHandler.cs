using System;

using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
{
    public class ExitCombatCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler
    {
        private const string ViableEventName = "ExitCombat";

        protected override bool CanHandleCommand(ICommand command)
        {
            return command.EventName == ExitCombatCommandResolutionHandler.ViableEventName;
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            combatStatisticsContainer.CurrentComabtStatistics.ExitCombatTime = DateTime.UtcNow;
            combatStatisticsContainer.CurrentComabtStatistics.IsCompleted = true;
            combatStatisticsContainer.CurrentComabtStatistics = null;

            return combatStatisticsContainer;
        }
    }
}
