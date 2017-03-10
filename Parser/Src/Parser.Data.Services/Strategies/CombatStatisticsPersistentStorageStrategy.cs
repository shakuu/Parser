using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.ViewModels;
using Parser.LogFileParser.Contracts;

namespace Parser.Data.Services.Strategies
{
    public class CombatStatisticsPersistentStorageStrategy : ICombatStatisticsPersistentStorageStrategy
    {
        private readonly IStoredCombatStatisticsDataProvider storedCombatStatisticsDataProvider;
        private readonly IEntityFrameworkTransactionFactory entityFrameworkTransactionFactory;
        private readonly IObjectMapperProvider objectMapperProvider;

        public CombatStatisticsPersistentStorageStrategy(IStoredCombatStatisticsDataProvider storedCombatStatisticsDataProvider, IEntityFrameworkTransactionFactory entityFrameworkTransactionFactory, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsDataProvider, nameof(IStoredCombatStatisticsDataProvider)).IsNull().Throw();
            Guard.WhenArgument(entityFrameworkTransactionFactory, nameof(IEntityFrameworkTransactionFactory)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsDataProvider = storedCombatStatisticsDataProvider;
            this.entityFrameworkTransactionFactory = entityFrameworkTransactionFactory;
            this.objectMapperProvider = objectMapperProvider;
        }

        public IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            Guard.WhenArgument(finalizedCombatStatistics, nameof(IFinalizedCombatStatistics)).IsNull().Throw();

            var combatStatisticsProjection = this.objectMapperProvider.Map<StoredCombatStatisticsViewModel>(finalizedCombatStatistics);

            using (var transaction = this.entityFrameworkTransactionFactory.CreateEntityFrameworkTransaction())
            {
                this.storedCombatStatisticsDataProvider.Create(combatStatisticsProjection);

                transaction.SaveChanges();
            }

            return finalizedCombatStatistics;
        }
    }
}
