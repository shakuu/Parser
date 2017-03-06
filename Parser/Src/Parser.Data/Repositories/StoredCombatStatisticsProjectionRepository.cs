using AutoMapper;

using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.Data.Repositories
{
    public class StoredCombatStatisticsProjectionRepository : IStoredCombatStatisticsProjectionRepository
    {
        private readonly IGenericRepository<StoredCombatStatistics> repo;
        private readonly IMapper objectMapper;

        public StoredCombatStatisticsProjectionRepository(IGenericRepository<StoredCombatStatistics> repo, IMapper objectMapper)
        {
            this.repo = repo;
            this.objectMapper = objectMapper;
        }

        public StoredCombatStatisticsProjection Create(StoredCombatStatisticsProjection entity)
        {
            var stats = this.objectMapper.Map<StoredCombatStatistics>(entity);

            this.repo.Create(stats);

            return entity;
        }
    }
}
