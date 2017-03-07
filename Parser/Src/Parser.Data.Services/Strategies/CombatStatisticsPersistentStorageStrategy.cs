using AutoMapper;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Projections;
using Parser.LogFileParser.Contracts;

namespace Parser.Data.Services.Strategies
{
    public class CombatStatisticsPersistentStorageStrategy : ICombatStatisticsPersistentStorageStrategy
    {
        private readonly IStoredCombatStatisticsProjectionRepository storedCombatStatisticsProjectionRepository;
        private readonly IBusinessTransactionFactory businessTransactionFactory;
        private readonly IMapper objectMapper;

        public CombatStatisticsPersistentStorageStrategy(IStoredCombatStatisticsProjectionRepository storedCombatStatisticsProjectionRepository, IBusinessTransactionFactory businessTransactionFactory, IMapper objectMapper)
        {
            Guard.WhenArgument(storedCombatStatisticsProjectionRepository, nameof(IStoredCombatStatisticsProjectionRepository)).IsNull().Throw();
            Guard.WhenArgument(businessTransactionFactory, nameof(IBusinessTransactionFactory)).IsNull().Throw();
            Guard.WhenArgument(objectMapper, nameof(IMapper)).IsNull().Throw();

            this.storedCombatStatisticsProjectionRepository = storedCombatStatisticsProjectionRepository;
            this.businessTransactionFactory = businessTransactionFactory;
            this.objectMapper = objectMapper;
        }

        public IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            Guard.WhenArgument(finalizedCombatStatistics, nameof(IFinalizedCombatStatistics)).IsNull().Throw();

            var combatStatisticsProjection = this.objectMapper.Map<StoredCombatStatisticsProjection>(finalizedCombatStatistics);

            using (var transaction = this.businessTransactionFactory.CreateBusinessTransaction())
            {
                this.storedCombatStatisticsProjectionRepository.Create(combatStatisticsProjection);

                transaction.Commit();
            }

            return finalizedCombatStatistics;
        }
    }
}
