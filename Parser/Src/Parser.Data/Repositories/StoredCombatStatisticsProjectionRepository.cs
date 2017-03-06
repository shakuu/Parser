using AutoMapper;

using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.Data.Repositories
{
    public class StoredCombatStatisticsProjectionRepository : IStoredCombatStatisticsProjectionRepository
    {
        private readonly IStoredCombatStatisticsRepository storedCombatStatisticsRepository;
        private readonly IMapper objectMapper;

        public StoredCombatStatisticsProjectionRepository(IStoredCombatStatisticsRepository storedCombatStatisticsRepository, IMapper objectMapper)
        {
            Guard.WhenArgument(storedCombatStatisticsRepository, nameof(IStoredCombatStatisticsRepository)).IsNull().Throw();
            Guard.WhenArgument(objectMapper, nameof(IMapper)).IsNull().Throw();

            this.storedCombatStatisticsRepository = storedCombatStatisticsRepository;
            this.objectMapper = objectMapper;
        }

        public StoredCombatStatisticsProjection Create(StoredCombatStatisticsProjection projection)
        {
            Guard.WhenArgument(projection, nameof(StoredCombatStatisticsProjection)).IsNull().Throw();

            var storedCombatStatistics = this.objectMapper.Map<StoredCombatStatistics>(projection);

            this.storedCombatStatisticsRepository.Create(storedCombatStatistics);

            return projection;
        }
    }
}
