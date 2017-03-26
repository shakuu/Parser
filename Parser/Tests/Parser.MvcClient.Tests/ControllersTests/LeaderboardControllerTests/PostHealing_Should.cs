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
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing(pageNumber))
                .ShouldRenderPartialView("_HealingDonePerSecondViewModelsPartial");

            // Assert
            leaderboardHealingService.Verify(s => s.GetTopStoredHealingOnPage(pageNumber + 1), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNull()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing(null))
                .ShouldRenderPartialView("_HealingDonePerSecondViewModelsPartial");

            // Assert
            leaderboardHealingService.Verify(s => s.GetTopStoredHealingOnPage(1), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNegative()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing(int.MinValue))
                .ShouldRenderPartialView("_HealingDonePerSecondViewModelsPartial");

            // Assert
            leaderboardHealingService.Verify(s => s.GetTopStoredHealingOnPage(1), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsIntMaxValue()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Healing(int.MaxValue))
                .ShouldRenderPartialView("_HealingDonePerSecondViewModelsPartial");

            // Assert
            leaderboardHealingService.Verify(s => s.GetTopStoredHealingOnPage(1), Times.Once);
        }

        [Test]
        public void RenderCorrectPartialViewWithCorrectViewModel()
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
                .WithCallTo(c => c.Healing(0))
                .ShouldRenderPartialView("_HealingDonePerSecondViewModelsPartial")
                .WithModel<HealingViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
