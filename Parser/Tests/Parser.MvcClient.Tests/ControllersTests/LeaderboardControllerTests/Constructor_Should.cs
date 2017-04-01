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
            var leaderboardService = new Mock<ILeaderboardService>();

            // Act
            var actualInstance = new LeaderboardController(leaderboardService.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<LoggingController>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenILeaderboardDamageServiceParameterIsNull()
        {
            // Arrange
            ILeaderboardService leaderboardService = null;

            // Act & Assert
            Assert.That(
                () => new LeaderboardController(leaderboardService),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILeaderboardService)));
        }
    }
}
