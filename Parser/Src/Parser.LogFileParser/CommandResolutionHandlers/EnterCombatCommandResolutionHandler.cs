using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.CommandResolutionHandlers.Base;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers
{
    public class EnterCombatCommandResolutionHandler : CommandResolutionHandler, ICommandResolutionHandler
    {
        private const string ViableEventName = "EnterCombat";

        private readonly ICombatStatisticsFactory combatStatisticsFactory;

        public EnterCombatCommandResolutionHandler(ICombatStatisticsFactory combatStatisticsFactory)
        {
            Guard.WhenArgument(combatStatisticsFactory, nameof(ICombatStatisticsFactory)).IsNull().Throw();

            this.combatStatisticsFactory = combatStatisticsFactory;
        }

        protected override bool CanHandleCommand(ICommand command)
        {
            if (string.IsNullOrEmpty(command.EventName))
            {
                return false;
            }
            else
            {
                return command.EventName == EnterCombatCommandResolutionHandler.ViableEventName;
            }
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            combatStatisticsContainer.CurrentComabtStatistics = this.combatStatisticsFactory.CreateCombatStatistics();
            combatStatisticsContainer.AllComabtStatistics.Add(combatStatisticsContainer.CurrentComabtStatistics);
            combatStatisticsContainer.CurrentComabtStatistics.EnterCombatTime = command.TimeStamp;

            return combatStatisticsContainer;
        }
    }
}
