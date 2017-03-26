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
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            var leaderboardController = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Act
            leaderboardController
                .WithCallTo(c => c.Damage())
                .ShouldRenderDefaultView();

            // Assert
            leaderboardDamageService.Verify(s => s.GetTopStoredDamageOnPage(0), Times.Once);
        }

        [Test]
        public void DisplayCorrectViewWithCorrectViewModel()
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
                .WithCallTo(c => c.Damage())
                .ShouldRenderDefaultView()
                .WithModel<DamageViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
