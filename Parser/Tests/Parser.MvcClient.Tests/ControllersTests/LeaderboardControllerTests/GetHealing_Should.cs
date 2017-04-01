using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using TestStack.FluentMVCTesting;

using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Leaderboard;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.LeaderboardControllerTests
{
    [TestFixture]
    public class GetHealing_Should
    {
        [Test]
        public void InvokeILeaderboardHealingService_GetTopStoredHealingOnPageMethodOnceWithCorrectParameters()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing())
                .ShouldRenderDefaultView();

            // Assert
            leaderboardService.Verify(s => s.GetTopHealingOnPage(0), Times.Once);
        }

        [Test]
        public void DisplayCorrectViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            var healingDonePerSecondViewModels = new List<OutputPerSecondViewModel>();
            var expectedViewModel = new LeaderboardViewModel(0, healingDonePerSecondViewModels);

            leaderboardService.Setup(s => s.GetTopHealingOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Healing())
                .ShouldRenderDefaultView()
                .WithModel<LeaderboardViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
