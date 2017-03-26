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
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing())
                .ShouldRenderDefaultView();

            // Assert
            leaderboardHealingService.Verify(s => s.GetTopStoredHealingOnPage(0), Times.Once);
        }

        [Test]
        public void DisplayCorrectViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            var healingDonePerSecondViewModels = new List<HealingDonePerSecondViewModel>();
            var expectedViewModel = new HealingViewModel(0, healingDonePerSecondViewModels);

            leaderboardHealingService.Setup(s => s.GetTopStoredHealingOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Healing())
                .ShouldRenderDefaultView()
                .WithModel<HealingViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
