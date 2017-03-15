using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.DataProviders
{
    public class DamageViewModelDataProvider
    {
        private const int DefaultPageSize = 10;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IDamageViewModelFactory damageViewModelFactory;
        private readonly IObjectMapperProvider objectMapperProvider;

        public DamageViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IDamageViewModelFactory damageViewModelFactory, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IDamageViewModelFactory)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.damageViewModelFactory = damageViewModelFactory;
            this.objectMapperProvider = objectMapperProvider;
        }

        DamageViewModel GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            if (pageNumber < 0)
            {
                pageNumber = 0;
            }

            var storedCombatStatistics = this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderBy(e => e.DamageDonePerSecond)
                .Skip(DamageViewModelDataProvider.DefaultPageSize * pageNumber)
                .Take(DamageViewModelDataProvider.DefaultPageSize)
                .ToList();

            var damageDonePerSecondViewModels = new LinkedList<DamageDonePerSecondViewModel>();
            foreach (var storedCombatStatistic in storedCombatStatistics)
            {
                var damageDonePerSecondViewModel = this.objectMapperProvider.Map<DamageDonePerSecondViewModel>(storedCombatStatistic);
                damageDonePerSecondViewModels.AddLast(damageDonePerSecondViewModel);
            }

            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(damageDonePerSecondViewModels);

            return damageViewModel;
        }
    }
}
