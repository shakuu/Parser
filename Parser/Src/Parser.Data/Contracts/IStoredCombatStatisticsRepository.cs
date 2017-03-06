using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.Data.Contracts
{
    public interface IStoredCombatStatisticsRepository : IGenericRepository<StoredCombatStatisticsProjection, StoredCombatStatistics>
    {
    }
}
