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
    public class PostHealing_Should
    {
        [TestCase(0)]
        [TestCase(42)]
        [TestCase(23)]
        [TestCase(17)]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter(int pageNumber)
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Healing(pageNumber));

            // Assert
            leaderboardService.Verify(s => s.GetTopHealingOnPage(pageNumber), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNull()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Healing(null));

            // Assert
            leaderboardService.Verify(s => s.GetTopHealingOnPage(0), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNegative()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Healing(int.MinValue));

            // Assert
            leaderboardService.Verify(s => s.GetTopHealingOnPage(0), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsIntMaxValue()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Healing(int.MaxValue));

            // Assert
            leaderboardService.Verify(s => s.GetTopHealingOnPage(0), Times.Once);
        }

        [Test]
        public void RenderCorrectPartialViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            var healingDonePerSecondViewModels = new List<OutputPerSecondViewModel>();
            var expectedViewModel = new LeaderboardViewModel(0, healingDonePerSecondViewModels);

            leaderboardService.Setup(s => s.GetTopHealingOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Healing(0))
                .ShouldRenderPartialView("_HealingAjaxFormPartial")
                .WithModel<LeaderboardViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
