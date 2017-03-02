using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
{
    public class ExitCombatCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler
    {
        private const string ViableEventName = "ExitCombat";

        private readonly IDateTimeProvider dateTimeProvider;

        public ExitCombatCommandResolutionHandler(IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.dateTimeProvider = dateTimeProvider;
        }

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
            combatStatisticsContainer.CurrentComabtStatistics.ExitCombatTime = this.dateTimeProvider.GetUtcNow();
            combatStatisticsContainer.CurrentComabtStatistics.IsCompleted = true;
            combatStatisticsContainer.CurrentComabtStatistics = null;

            return combatStatisticsContainer;
        }
    }
}
