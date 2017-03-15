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
    public class DamageViewModelDataProvider : IDamageViewModelDataProvider
    {
        private const int DefaultPageSize = 50;

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

        public DamageViewModel GetDamageViewModelOnPage(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var storedCombatStatistics = this.GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(pageNumber);
            pageNumber = storedCombatStatistics.Count / DamageViewModelDataProvider.DefaultPageSize;

            var damageDonePerSecondViewModels = new LinkedList<DamageDonePerSecondViewModel>();
            foreach (var storedCombatStatistic in storedCombatStatistics)
            {
                var damageDonePerSecondViewModel = this.objectMapperProvider.Map<DamageDonePerSecondViewModel>(storedCombatStatistic);
                damageDonePerSecondViewModels.AddLast(damageDonePerSecondViewModel);
            }

            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(pageNumber, damageDonePerSecondViewModels);

            return damageViewModel;
        }

        private ICollection<StoredCombatStatistics> GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.DamageDonePerSecond)
                .Take(DamageViewModelDataProvider.DefaultPageSize * pageNumber)
                .ToList();
        }
    }
}
