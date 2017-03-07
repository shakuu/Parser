using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.Data.Repositories
{
    public class StoredCombatStatisticsProjectionRepository : IStoredCombatStatisticsProjectionRepository
    {
        private readonly IStoredCombatStatisticsRepository storedCombatStatisticsRepository;
        private readonly IObjectMapperProvider objectMapperProvider;

        public StoredCombatStatisticsProjectionRepository(IStoredCombatStatisticsRepository storedCombatStatisticsRepository, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsRepository, nameof(IStoredCombatStatisticsRepository)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsRepository = storedCombatStatisticsRepository;
            this.objectMapperProvider = objectMapperProvider;
        }

        public StoredCombatStatisticsProjection Create(StoredCombatStatisticsProjection projection)
        {
            Guard.WhenArgument(projection, nameof(StoredCombatStatisticsProjection)).IsNull().Throw();

            var storedCombatStatistics = this.objectMapperProvider.Map<StoredCombatStatistics>(projection);

            this.storedCombatStatisticsRepository.Create(storedCombatStatistics);

            return projection;
        }
    }
}
