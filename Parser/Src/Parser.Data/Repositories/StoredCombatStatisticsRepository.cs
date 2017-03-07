using Parser.Data.Contracts;
using Parser.Data.Models;

namespace Parser.Data.Repositories
{
    public class StoredCombatStatisticsRepository : GenericRepository<StoredCombatStatistics>, IStoredCombatStatisticsRepository
    {
        public StoredCombatStatisticsRepository(IParserDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
