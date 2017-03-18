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
    public class HealingViewModelDataProvider : IHealingViewModelDataProvider
    {
        private const int DefaultSvgElementSize = 300;
        private const int DefaultPercentageBarRadius = 75;
        private const int DefaultPageSize = 5;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IProgressPartialCircleSvgPathStringProvider progressPartialCircleSvgPathStringProvider;
        private readonly IHealingViewModelFactory damageViewModelFactory;
        private readonly IObjectMapperProvider objectMapperProvider;

        public HealingViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IProgressPartialCircleSvgPathStringProvider progressPartialCircleSvgPathStringProvider, IHealingViewModelFactory damageViewModelFactory, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(progressPartialCircleSvgPathStringProvider, nameof(IProgressPartialCircleSvgPathStringProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IHealingViewModelFactory)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.progressPartialCircleSvgPathStringProvider = progressPartialCircleSvgPathStringProvider;
            this.damageViewModelFactory = damageViewModelFactory;
            this.objectMapperProvider = objectMapperProvider;
        }

        public HealingViewModel GetDamageViewModelOnPage(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var storedCombatStatistics = this.GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(pageNumber);
            pageNumber = storedCombatStatistics.Count / HealingViewModelDataProvider.DefaultPageSize;

            var damageDonePerSecondViewModels = new List<HealingDonePerSecondViewModel>();
            foreach (var storedCombatStatistic in storedCombatStatistics)
            {
                var damageDonePerSecondViewModel = this.objectMapperProvider.Map<HealingDonePerSecondViewModel>(storedCombatStatistic);
                damageDonePerSecondViewModels.Add(damageDonePerSecondViewModel);
            }

            var damageViewModel = this.damageViewModelFactory.CreateHealingViewModell(pageNumber, damageDonePerSecondViewModels);
            foreach (var viewModel in damageViewModel.HealingDonePerSecondViewModels)
            {
                viewModel.SvgString = this.progressPartialCircleSvgPathStringProvider.GetPathString(viewModel.PercentageOfBest, HealingViewModelDataProvider.DefaultPercentageBarRadius, HealingViewModelDataProvider.DefaultSvgElementSize);
            }

            return damageViewModel;
        }

        private ICollection<StoredCombatStatistics> GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            // TODO: Refactor with SELECT/ Project
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.DamageDonePerSecond)
                .Take(HealingViewModelDataProvider.DefaultPageSize * pageNumber)
                .ToList();
        }
    }
}
