using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Html.Svg;
using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardDamageService : ILeaderboardDamageService
    {
        private const int DefaultSvgElementSize = 300;
        private const int DefaultPercentageBarRadius = 75;
        private const int DefaultPageSize = 5;
        private const int DefaultPageNumber = 1;

        private readonly IDamageViewModelDataProvider damageViewModelDataProvider;
        private readonly IDamageViewModelFactory damageViewModelFactory;
        private readonly IPartialCircleSvgPathProvider partialCircleSvgPathProvider;

        public LeaderboardDamageService(IDamageViewModelDataProvider damageViewModelDataProvider, IDamageViewModelFactory damageViewModelFactory, IPartialCircleSvgPathProvider partialCircleSvgPathProvider)
        {
            Guard.WhenArgument(damageViewModelDataProvider, nameof(IDamageViewModelDataProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IDamageViewModelFactory)).IsNull().Throw();
            Guard.WhenArgument(partialCircleSvgPathProvider, nameof(IPartialCircleSvgPathProvider)).IsNull().Throw();

            this.damageViewModelDataProvider = damageViewModelDataProvider;
            this.damageViewModelFactory = damageViewModelFactory;
            this.partialCircleSvgPathProvider = partialCircleSvgPathProvider;
        }

        public DamageViewModel GetTopStoredDamageOnPage(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = LeaderboardDamageService.DefaultPageNumber;
            }

            var damageViewModels = new List<DamageViewModel>();
            for (int pageIndex = 1; pageIndex <= pageNumber; pageIndex++)
            {
                var resultDamageViewModel = this.damageViewModelDataProvider.GetDamageViewModelOnPage(pageIndex, LeaderboardDamageService.DefaultPageSize);
                damageViewModels.Add(resultDamageViewModel);
            }

            var damageDonePersecondViewModels = new List<DamageDonePerSecondViewModel>();
            foreach (var viewModel in damageViewModels)
            {
                damageDonePersecondViewModels.AddRange(viewModel.DamageDonePerSecondViewModels);
            }

            pageNumber = damageDonePersecondViewModels.Count / LeaderboardDamageService.DefaultPageSize;
            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(pageNumber, damageViewModels[0].MaximumDamageDonePerSecond, damageDonePersecondViewModels);

            foreach (var viewModel in damageViewModel.DamageDonePerSecondViewModels)
            {
                viewModel.SvgString = this.partialCircleSvgPathProvider.GetSvgPath(viewModel.PercentageOfBest, LeaderboardDamageService.DefaultPercentageBarRadius, LeaderboardDamageService.DefaultSvgElementSize);
            }

            return damageViewModel;
        }
    }
}
