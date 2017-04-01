using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Html.Svg;
using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private const int DefaultSvgElementSize = 300;
        private const int DefaultPercentageBarRadius = 75;
        private const int DefaultPageSize = 5;
        private const int DefaultPageNumber = 1;

        private readonly IOutputPerSecondViewModelDataProvider outputPerSecondViewModelDataProvider;
        private readonly IPartialCircleSvgPathProvider partialCircleSvgPathProvider;
        private readonly ILeaderboardViewModelFactory leaderboardViewModelFactory;

        public LeaderboardService(IOutputPerSecondViewModelDataProvider outputPerSecondViewModelDataProvider, IPartialCircleSvgPathProvider partialCircleSvgPathProvider, ILeaderboardViewModelFactory leaderboardViewModelFactory)
        {
            Guard.WhenArgument(outputPerSecondViewModelDataProvider, nameof(IOutputPerSecondViewModelDataProvider)).IsNull().Throw();
            Guard.WhenArgument(partialCircleSvgPathProvider, nameof(IPartialCircleSvgPathProvider)).IsNull().Throw();
            Guard.WhenArgument(leaderboardViewModelFactory, nameof(ILeaderboardViewModelFactory)).IsNull().Throw();

            this.outputPerSecondViewModelDataProvider = outputPerSecondViewModelDataProvider;
            this.partialCircleSvgPathProvider = partialCircleSvgPathProvider;
            this.leaderboardViewModelFactory = leaderboardViewModelFactory;
        }

        public LeaderboardViewModel GetTopDamageOnPage(int pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);
            var outputPerSecondViewModels = this.GetOutputPerSecondViewModelsOnPage(this.outputPerSecondViewModelDataProvider.GetTopDamageOnPage, pageNumber);
            outputPerSecondViewModels = this.GetSvgPathForOutputPerSecondViewModels(outputPerSecondViewModels, outputPerSecondViewModels[0].OutputPerSecond);

            return this.CreateLeaderboardViewModel(pageNumber, outputPerSecondViewModels);
        }

        public LeaderboardViewModel GetTopHealingOnPage(int pageNumber)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);
            var outputPerSecondViewModels = this.GetOutputPerSecondViewModelsOnPage(this.outputPerSecondViewModelDataProvider.GetTopHealingOnPage, pageNumber);
            outputPerSecondViewModels = this.GetSvgPathForOutputPerSecondViewModels(outputPerSecondViewModels, outputPerSecondViewModels[0].OutputPerSecond);

            return this.CreateLeaderboardViewModel(pageNumber, outputPerSecondViewModels);
        }

        private IList<OutputPerSecondViewModel> GetOutputPerSecondViewModelsOnPage(Func<int, int, IList<OutputPerSecondViewModel>> dataProviderMethod, int pageNumber)
        {
            var outputPerSecondViewModels = new List<OutputPerSecondViewModel>();
            for (int pageIndex = 1; pageIndex <= pageNumber; pageIndex++)
            {
                var resultDamageViewModel = dataProviderMethod.Invoke(pageIndex, LeaderboardService.DefaultPageSize);
                outputPerSecondViewModels.AddRange(resultDamageViewModel);
            }

            return outputPerSecondViewModels;
        }

        private IList<OutputPerSecondViewModel> GetSvgPathForOutputPerSecondViewModels(IList<OutputPerSecondViewModel> outputPerSecondViewModels, double maximumValue)
        {
            foreach (var viewModel in outputPerSecondViewModels)
            {
                var percentage = (int)(viewModel.OutputPerSecond / maximumValue * 100);
                if (percentage < 0)
                {
                    percentage = 0;
                }

                viewModel.PercentageOfBest = percentage;
                viewModel.SvgString = this.partialCircleSvgPathProvider.GetSvgPath(viewModel.PercentageOfBest, LeaderboardService.DefaultPercentageBarRadius, LeaderboardService.DefaultSvgElementSize);
            }

            return outputPerSecondViewModels;
        }

        private LeaderboardViewModel CreateLeaderboardViewModel(int pageNumber, IList<OutputPerSecondViewModel> outputPerSecondViewModels)
        {
            pageNumber = outputPerSecondViewModels.Count / LeaderboardService.DefaultPageSize;
            return this.leaderboardViewModelFactory.CreateLeaderboardViewModel(pageNumber, outputPerSecondViewModels);
        }

        private int ValidatePageNumber(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = LeaderboardService.DefaultPageNumber;
            }

            return pageNumber;
        }
    }
}
