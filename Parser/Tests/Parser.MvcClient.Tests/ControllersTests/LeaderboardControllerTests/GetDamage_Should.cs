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
    public class GetDamage_Should
    {
        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameters()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Damage())
                .ShouldRenderDefaultView();

            // Assert
            leaderboardService.Verify(s => s.GetTopDamageOnPage(0), Times.Once);
        }

        [Test]
        public void DisplayCorrectViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            var damageDonePerSecondViewModels = new List<OutputPerSecondViewModel>();
            var expectedViewModel = new LeaderboardViewModel(0, damageDonePerSecondViewModels);

            leaderboardService.Setup(s => s.GetTopDamageOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Damage())
                .ShouldRenderDefaultView()
                .WithModel<LeaderboardViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
