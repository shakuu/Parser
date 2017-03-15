using System.Linq;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.DataProviders
{
    public class DamageViewModelDataProvider
    {
        private const int DefaultPageSize = 10;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IObjectMapperProvider objectMapperProvider;

        public DamageViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.objectMapperProvider = objectMapperProvider;
        }

        DamageViewModel GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            var storedCombatStatistics = this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderBy(e => e.DamageDonePerSecond)
                .Skip(DamageViewModelDataProvider.DefaultPageSize * pageNumber)
                .Take(DamageViewModelDataProvider.DefaultPageSize)
                .ToList();

            return null;
        }
    }
}
