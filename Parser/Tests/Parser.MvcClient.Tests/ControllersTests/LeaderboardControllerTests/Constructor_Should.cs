using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Services.Contracts;
using Parser.MvcClient.Controllers;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Tests.ControllersTests.LeaderboardControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            // Act
            var actualInstance = new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<LoggingController>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenILeaderboardDamageServiceParameterIsNull()
        {
            // Arrange
            ILeaderboardDamageService leaderboardDamageService = null;
            var leaderboardHealingService = new Mock<ILeaderboardHealingService>();

            // Act & Assert
            Assert.That(
                () => new LeaderboardController(leaderboardDamageService, leaderboardHealingService.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILeaderboardDamageService)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenILeaderboardHealingServiceParameterIsNull()
        {
            // Arrange
            var leaderboardDamageService = new Mock<ILeaderboardDamageService>();
            ILeaderboardHealingService leaderboardHealingService = null;

            // Act & Assert
            Assert.That(
                () => new LeaderboardController(leaderboardDamageService.Object, leaderboardHealingService),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILeaderboardHealingService)));
        }
    }
}
