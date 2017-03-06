using AutoMapper;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Projections;
using Parser.LogFileParser.Contracts;

namespace Parser.Data.Services.Strategies
{
    public class CombatStatisticsPersistentStorageStrategy : ICombatStatisticsPersistentStorageStrategy
    {
        private readonly IStoredCombatStatisticsProjectionRepository storedCombatStatisticsProjectionRepository;
        private readonly IMapper objectMapper;

        public CombatStatisticsPersistentStorageStrategy(IStoredCombatStatisticsProjectionRepository storedCombatStatisticsProjectionRepository, IMapper objectMapper)
        {
            Guard.WhenArgument(storedCombatStatisticsProjectionRepository, nameof(IStoredCombatStatisticsProjectionRepository)).IsNull().Throw();
            Guard.WhenArgument(objectMapper, nameof(IMapper)).IsNull().Throw();

            this.storedCombatStatisticsProjectionRepository = storedCombatStatisticsProjectionRepository;
            this.objectMapper = objectMapper;
        }

        public IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            var combatStatisticsProjection = this.objectMapper.Map<StoredCombatStatisticsProjection>(finalizedCombatStatistics);

            this.storedCombatStatisticsProjectionRepository.Create(combatStatisticsProjection);

            return finalizedCombatStatistics;
        }
    }
}
