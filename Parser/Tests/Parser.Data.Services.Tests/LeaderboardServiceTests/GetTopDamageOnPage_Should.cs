using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Parser.Common.Html.Svg;
using Parser.Data.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services.Tests.LeaderboardServiceTests
{
    [TestFixture]
    public class GetTopDamageOnPage_Should
    {
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(42)]
        [TestCase(123)]
        public void InvokeIOutputPerSecondViewModelDataProvider_GetTopDamageOnPageInDescendingOrderAsManyTimesAsRequestedPages(int pageNumber)
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            var leaderboardService = new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory.Object);

            var outputPerSecondViewModels = new List<OutputPerSecondViewModel>();
            outputPerSecondViewModelDataProvider.Setup(p => p.GetTopDamageOnPageInDescendingOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(outputPerSecondViewModels);

            // Act
            leaderboardService.GetTopDamageOnPage(pageNumber);

            // Assert
            outputPerSecondViewModelDataProvider.Verify(p => p.GetTopDamageOnPageInDescendingOrder(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(pageNumber + 1));
        }

        [Test]
        public void InvokeIPartialCircleSvgPathProvider_GetSvgPathMethodAsManyTimesAsAvailableOutputPerSecondViewModels()
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            var leaderboardService = new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory.Object);

            var outputPerSecondViewModels = new List<OutputPerSecondViewModel>()
            {
                new OutputPerSecondViewModel(),
                new OutputPerSecondViewModel(),
                new OutputPerSecondViewModel(),
                new OutputPerSecondViewModel(),
                new OutputPerSecondViewModel(),

            };
            outputPerSecondViewModelDataProvider.Setup(p => p.GetTopDamageOnPageInDescendingOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(outputPerSecondViewModels);

            var pageNumber = 0;

            var expectedInvocationsCount = outputPerSecondViewModels.Count;

            // Act
            leaderboardService.GetTopDamageOnPage(pageNumber);

            // Assert
            partialCircleSvgPathProvider.Verify(p => p.GetSvgPath(It.IsAny<int>(), 75, 300), Times.Exactly(expectedInvocationsCount));
        }

        [Test]
        public void InvokeILeaderboardViewModelFactory_CreateLeaderboardViewModelMethodOnce()
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            var leaderboardService = new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory.Object);

            var outputPerSecondViewModels = new List<OutputPerSecondViewModel>();
            outputPerSecondViewModelDataProvider.Setup(p => p.GetTopDamageOnPageInDescendingOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(outputPerSecondViewModels);

            var pageNumber = 1;

            // Act
            leaderboardService.GetTopDamageOnPage(pageNumber);

            // Assert
            leaderboardViewModelFactory.Verify(f => f.CreateLeaderboardViewModel(It.IsAny<int>(), It.IsAny<IList<OutputPerSecondViewModel>>()), Times.Once);
        }
    }
}
