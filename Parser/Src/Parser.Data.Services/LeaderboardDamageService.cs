using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardDamageService : ILeaderboardDamageService
    {
        private const int DefaultPageSize = 5;
        private const int DefaultPageNumber = 1;

        private readonly IDamageViewModelDataProvider damageViewModelDataProvider;
        private readonly IDamageViewModelFactory damageViewModelFactory;

        public LeaderboardDamageService(IDamageViewModelDataProvider damageViewModelDataProvider, IDamageViewModelFactory damageViewModelFactory)
        {
            Guard.WhenArgument(damageViewModelDataProvider, nameof(IDamageViewModelDataProvider)).IsNull().Throw();
            Guard.WhenArgument(damageViewModelFactory, nameof(IDamageViewModelFactory)).IsNull().Throw();

            this.damageViewModelDataProvider = damageViewModelDataProvider;
            this.damageViewModelFactory = damageViewModelFactory;
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

            pageNumber =  damageDonePersecondViewModels.Count / LeaderboardDamageService. DefaultPageSize;
            var damageViewModel = this.damageViewModelFactory.CreateDamageViewModel(pageNumber, damageViewModels[0].MaximumDamageDonePerSecond, damageDonePersecondViewModels);

            return damageViewModel;
        }
    }
}
