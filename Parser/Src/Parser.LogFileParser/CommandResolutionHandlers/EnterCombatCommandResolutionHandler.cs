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
        private readonly IDateTimeProvider dateTimeProvider;

        public EnterCombatCommandResolutionHandler(ICombatStatisticsFactory combatStatisticsFactory, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(combatStatisticsFactory, nameof(ICombatStatisticsFactory)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.combatStatisticsFactory = combatStatisticsFactory;
            this.dateTimeProvider = dateTimeProvider;
        }

        protected override bool CanHandleCommand(ICommand command)
        {
            return command.EventName == EnterCombatCommandResolutionHandler.ViableEventName;
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            combatStatisticsContainer.CurrentComabtStatistics = this.combatStatisticsFactory.CreateCombatStatistics();
            combatStatisticsContainer.CurrentComabtStatistics.EnterCombatTime = this.dateTimeProvider.GetUtcNow();
            combatStatisticsContainer.AllComabtStatistics.Add(combatStatisticsContainer.CurrentComabtStatistics);

            return combatStatisticsContainer;
        }
    }
}
