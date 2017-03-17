using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Html.Svg;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.DataProviders
{
    public class DamageViewModelDataProvider : IDamageViewModelDataProvider
    {
        private const int DefaultSvgElementSize = 300;
        private const int DefaultPercentageBarRadius = 75;
        private const int DefaultPageSize = 5;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IProgressPartialCircleSvgPathStringProvider progressPartialCircleSvgPathStringProvider;
        private readonly IDamageViewModelFactory damageViewModelFactory;
        private readonly IObjectMapperProvider objectMapperProvider;

        public DamageViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IProgressPartialCircleSvgPathStringProvider progressPartialCircleSvgPathStringProvider, IDamageViewModelFactory damageViewModelFactory, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(progressPartialCircleSvgPathStringProvider, nameof(IProgressPartialCircleSvgPathStringProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IDamageViewModelFactory)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.progressPartialCircleSvgPathStringProvider = progressPartialCircleSvgPathStringProvider;
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

            var damageDonePerSecondViewModels = new List<DamageDonePerSecondViewModel>();
            foreach (var storedCombatStatistic in storedCombatStatistics)
            {
                var damageDonePerSecondViewModel = this.objectMapperProvider.Map<DamageDonePerSecondViewModel>(storedCombatStatistic);
                damageDonePerSecondViewModels.Add(damageDonePerSecondViewModel);
            }

            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(pageNumber, damageDonePerSecondViewModels);
            foreach (var viewModel in damageViewModel.DamageDonePerSecondViewModels)
            {
                viewModel.SvgString = this.progressPartialCircleSvgPathStringProvider.GetPathString(viewModel.PercentageOfBest, DamageViewModelDataProvider.DefaultPercentageBarRadius, DamageViewModelDataProvider.DefaultSvgElementSize);
            }

            return damageViewModel;
        }

        private ICollection<StoredCombatStatistics> GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            // TODO: Refactor with SELECT/ Project
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.DamageDonePerSecond)
                .Take(DamageViewModelDataProvider.DefaultPageSize * pageNumber)
                .ToList();
        }
    }
}
