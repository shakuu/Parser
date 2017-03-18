using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

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

        public HealingViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IProgressPartialCircleSvgPathStringProvider progressPartialCircleSvgPathStringProvider, IHealingViewModelFactory damageViewModelFactory)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(progressPartialCircleSvgPathStringProvider, nameof(IProgressPartialCircleSvgPathStringProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IHealingViewModelFactory)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.progressPartialCircleSvgPathStringProvider = progressPartialCircleSvgPathStringProvider;
            this.damageViewModelFactory = damageViewModelFactory;
        }

        public HealingViewModel GetHealingViewModelOnPage(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var healingDonePerSecondViewModels = this.GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(pageNumber);
            pageNumber = healingDonePerSecondViewModels.Count / HealingViewModelDataProvider.DefaultPageSize;

            var damageViewModel = this.damageViewModelFactory.CreateHealingViewModell(pageNumber, healingDonePerSecondViewModels);
            foreach (var viewModel in damageViewModel.HealingDonePerSecondViewModels)
            {
                viewModel.SvgString = this.progressPartialCircleSvgPathStringProvider.GetPathString(viewModel.PercentageOfBest, HealingViewModelDataProvider.DefaultPercentageBarRadius, HealingViewModelDataProvider.DefaultSvgElementSize);
            }

            return damageViewModel;
        }

        private IList<HealingDonePerSecondViewModel> GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.HealingDonePerSecond)
                .Take(HealingViewModelDataProvider.DefaultPageSize * pageNumber)
                .Select(e => new HealingDonePerSecondViewModel() { Id = e.Id, CharacterName = e.CharacterName, HealingDonePerSecond = e.HealingDonePerSecond })
                .ToList();
        }
    }
}
