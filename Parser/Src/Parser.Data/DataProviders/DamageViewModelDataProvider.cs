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
    public class DamageViewModelDataProvider : IDamageViewModelDataProvider
    {
        private const int DefaultSvgElementSize = 300;
        private const int DefaultPercentageBarRadius = 75;
        private const int DefaultPageSize = 5;
        private const int DefaultPageNumber = 1;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IPartialCircleSvgPathProvider partialCircleSvgPathProvider;
        private readonly IDamageViewModelFactory damageViewModelFactory;

        public DamageViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IPartialCircleSvgPathProvider partialCircleSvgPathProvider, IDamageViewModelFactory damageViewModelFactory)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(partialCircleSvgPathProvider, nameof(IPartialCircleSvgPathProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IDamageViewModelFactory)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.partialCircleSvgPathProvider = partialCircleSvgPathProvider;
            this.damageViewModelFactory = damageViewModelFactory;
        }

        public DamageViewModel GetDamageViewModelOnPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = DamageViewModelDataProvider.DefaultPageNumber;
            }

            if (pageSize <= 0)
            {
                pageSize = DamageViewModelDataProvider.DefaultPageSize;
            }

            var damageDonePerSecondViewModels = this.GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(pageNumber, pageSize);
            pageNumber = damageDonePerSecondViewModels.Count / pageSize;

            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(pageNumber, damageDonePerSecondViewModels);
            foreach (var viewModel in damageViewModel.DamageDonePerSecondViewModels)
            {
                viewModel.SvgString = this.partialCircleSvgPathProvider.GetSvgPath(viewModel.PercentageOfBest, DamageViewModelDataProvider.DefaultPercentageBarRadius, DamageViewModelDataProvider.DefaultSvgElementSize);
            }

            return damageViewModel;
        }

        private IList<DamageDonePerSecondViewModel> GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber, int pageSize)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.DamageDonePerSecond)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .Select(e => new DamageDonePerSecondViewModel() { Id = e.Id, CharacterName = e.CharacterName, DamageDonePerSecond = e.DamageDonePerSecond })
                .ToList();
        }
    }
}
