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
        private const int DefaultPageNumber = 1;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IPartialCircleSvgPathProvider partialCircleSvgPathProvider;
        private readonly IHealingViewModelFactory healingViewModelFactory;

        public HealingViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IPartialCircleSvgPathProvider partialCircleSvgPathProvider, IHealingViewModelFactory healingViewModelFactory)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(partialCircleSvgPathProvider, nameof(IPartialCircleSvgPathProvider)).IsNull().Throw();
            Guard.WhenArgument(healingViewModelFactory, nameof(IHealingViewModelFactory)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.partialCircleSvgPathProvider = partialCircleSvgPathProvider;
            this.healingViewModelFactory = healingViewModelFactory;
        }

        public HealingViewModel GetHealingViewModelOnPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = HealingViewModelDataProvider.DefaultPageNumber;
            }

            if (pageSize <= 0)
            {
                pageSize = HealingViewModelDataProvider.DefaultPageSize;
            }

            var healingDonePerSecondViewModels = this.GetTopStoredCombatStatisticsByHealingDonePerSecondOnPage(pageNumber, pageSize);
            pageNumber = healingDonePerSecondViewModels.Count / pageSize;

            var healingViewModel = this.healingViewModelFactory.CreateHealingViewModell(pageNumber, healingDonePerSecondViewModels);
            foreach (var viewModel in healingViewModel.HealingDonePerSecondViewModels)
            {
                viewModel.SvgString = this.partialCircleSvgPathProvider.GetSvgPath(viewModel.PercentageOfBest, HealingViewModelDataProvider.DefaultPercentageBarRadius, HealingViewModelDataProvider.DefaultSvgElementSize);
            }

            return healingViewModel;
        }

        private IList<HealingDonePerSecondViewModel> GetTopStoredCombatStatisticsByHealingDonePerSecondOnPage(int pageNumber, int pageSize)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.HealingDonePerSecond)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .Select(e => new HealingDonePerSecondViewModel() { Id = e.Id, CharacterName = e.CharacterName, HealingDonePerSecond = e.HealingDonePerSecond })
                .ToList();
        }
    }
}
