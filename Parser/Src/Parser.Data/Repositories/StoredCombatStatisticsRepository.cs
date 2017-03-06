using AutoMapper;

using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.Data.Repositories
{
    public class StoredCombatStatisticsRepository : GenericRepository<StoredCombatStatisticsProjection, StoredCombatStatistics>, IStoredCombatStatisticsRepository, IGenericRepository<StoredCombatStatisticsProjection, StoredCombatStatistics>
    {
        public StoredCombatStatisticsRepository(IParserDbContext dbContext, IMapper objectMapper)
            : base(dbContext, objectMapper)
        {
        }
    }
}
