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
    public class PostDamage_Should
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
                .WithCallTo(c => c.Damage(pageNumber))
                .ShouldRenderPartialView("_DamageDonePerSecondViewModelsPartial");

            // Assert
            leaderboardDamageService.Verify(s => s.GetTopStoredDamageOnPage(pageNumber + 1), Times.Once);
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
                .WithCallTo(c => c.Damage(null))
                .ShouldRenderPartialView("_DamageDonePerSecondViewModelsPartial");

            // Assert
            leaderboardDamageService.Verify(s => s.GetTopStoredDamageOnPage(1), Times.Once);
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
                .WithCallTo(c => c.Damage(int.MinValue))
                .ShouldRenderPartialView("_DamageDonePerSecondViewModelsPartial");

            // Assert
            leaderboardDamageService.Verify(s => s.GetTopStoredDamageOnPage(1), Times.Once);
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
                .WithCallTo(c => c.Damage(int.MaxValue))
                .ShouldRenderPartialView("_DamageDonePerSecondViewModelsPartial");

            // Assert
            leaderboardDamageService.Verify(s => s.GetTopStoredDamageOnPage(1), Times.Once);
        }

        [Test]
        public void RenderCorrectPartialViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            var damageDonePerSecondViewModels = new List<DamageDonePerSecondViewModel>();
            var expectedViewModel = new DamageViewModel(0, damageDonePerSecondViewModels);

            leaderboardDamageService.Setup(s => s.GetTopStoredDamageOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Damage(0))
                .ShouldRenderPartialView("_DamageDonePerSecondViewModelsPartial")
                .WithModel<DamageViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
