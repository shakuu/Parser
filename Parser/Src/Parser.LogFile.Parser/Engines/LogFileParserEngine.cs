using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        private readonly ICommandResolutionHandler commandResolutionHandler;
        private readonly ICombatStatisticsFinalizationStrategy combatStatisticsFinalizationStrategy;
        private readonly ICombatStatisticsPersistentStorageStrategy combatStatisticsPersistentStorageStrategy;
        private readonly ILiveCombatStatisticsCreationStrategy liveCombatStatisticsCreationStrategy;

        private ICombatStatisticsContainer combatStatisticsContainer;

        public LogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainer combatStatisticsContainer, ICombatStatisticsFinalizationStrategy combatStatisticsFinalizationStrategy, ICombatStatisticsPersistentStorageStrategy combatStatisticsPersistentStorageStrategy, ILiveCombatStatisticsCreationStrategy liveCombatStatisticsCreationStrategy)
        {
            Guard.WhenArgument(commandResolutionHandler, nameof(ICommandResolutionHandler)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsContainer, nameof(ICombatStatisticsContainer)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsFinalizationStrategy, nameof(ICombatStatisticsFinalizationStrategy)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsPersistentStorageStrategy, nameof(ICombatStatisticsPersistentStorageStrategy)).IsNull().Throw();
            Guard.WhenArgument(liveCombatStatisticsCreationStrategy, nameof(ILiveCombatStatisticsCreationStrategy)).IsNull().Throw();

            this.commandResolutionHandler = commandResolutionHandler;
            this.combatStatisticsContainer = combatStatisticsContainer;
            this.combatStatisticsFinalizationStrategy = combatStatisticsFinalizationStrategy;
            this.combatStatisticsPersistentStorageStrategy = combatStatisticsPersistentStorageStrategy;
            this.liveCombatStatisticsCreationStrategy = liveCombatStatisticsCreationStrategy;

            this.combatStatisticsContainer.OnCurrentCombatChanged += this.OnCurrentCombatStatisticsChanged;
        }

        protected ICombatStatisticsContainer CombatStatisticsContainer { get { return this.combatStatisticsContainer; } set { this.combatStatisticsContainer = value; } }

        public void EnqueueCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            this.combatStatisticsContainer = this.commandResolutionHandler.ResolveCommand(command, this.combatStatisticsContainer);
        }

        public ILiveCombatStatistics GetLiveCombatStatistics()
        {
            return this.liveCombatStatisticsCreationStrategy.CreateLiveCombatStatistics(this.combatStatisticsContainer.CurrentCombatStatistics);
        }

        protected void OnCurrentCombatStatisticsChanged(object sender, CurrentCombatStatisticsChangedEventArgs args)
        {
            Guard.WhenArgument(args, nameof(CurrentCombatStatisticsChangedEventArgs)).IsNull().Throw();
            Guard.WhenArgument(args.CombatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var finalizedCombatStatistics = this.combatStatisticsFinalizationStrategy.FinalizeCombatStatistics(args.CombatStatistics);
            var storedCombatStatistics = this.combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics);
        }
    }
}
