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
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Damage(pageNumber));

            // Assert
            leaderboardService.Verify(s => s.GetTopDamageOnPage(pageNumber), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNull()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Damage(null));

            // Assert
            leaderboardService.Verify(s => s.GetTopDamageOnPage(0), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsNegative()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Damage(int.MinValue));

            // Assert
            leaderboardService.Verify(s => s.GetTopDamageOnPage(0), Times.Once);
        }

        [Test]
        public void InvokeILeaderboardDamageService_GetTopStoredDamageOnPageMethodOnceWithCorrectParameter_WhenPageNumberIsIntMaxValue()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            // Act
            leaderboardController.WithCallTo(c => c.Damage(int.MaxValue));

            // Assert
            leaderboardService.Verify(s => s.GetTopDamageOnPage(0), Times.Once);
        }

        [Test]
        public void RenderCorrectPartialViewWithCorrectViewModel()
        {
            // Arrange
            var leaderboardService = new Mock<ILeaderboardService>();

            var leaderboardController = new LeaderboardController(leaderboardService.Object);

            var damageDonePerSecondViewModels = new List<OutputPerSecondViewModel>();
            var expectedViewModel = new LeaderboardViewModel(0, damageDonePerSecondViewModels);

            leaderboardService.Setup(s => s.GetTopDamageOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            leaderboardController
                .WithCallTo(c => c.Damage(0))
                .ShouldRenderPartialView("_DamageAjaxFormPartial")
                .WithModel<LeaderboardViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
