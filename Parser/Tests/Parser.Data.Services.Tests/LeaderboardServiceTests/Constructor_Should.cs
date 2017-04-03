using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Html.Svg;
using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.Data.Services.Tests.LeaderboardServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            // Act
            var actualInstance = new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ILeaderboardService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIOutputPerSecondViewModelDataProviderParameterIsNull()
        {
            // Arrange
            IOutputPerSecondViewModelDataProvider outputPerSecondViewModelDataProvider = null;
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            // Act & Assert
            Assert.That(
                () => new LeaderboardService(outputPerSecondViewModelDataProvider, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IOutputPerSecondViewModelDataProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPartialCircleSvgPathProviderParameterIsNull()
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            IPartialCircleSvgPathProvider partialCircleSvgPathProvider = null;
            var leaderboardViewModelFactory = new Mock<ILeaderboardViewModelFactory>();

            // Act & Assert
            Assert.That(
                () => new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider, leaderboardViewModelFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IPartialCircleSvgPathProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenILeaderboardViewModelFactoryParameterIsNull()
        {
            // Arrange
            var outputPerSecondViewModelDataProvider = new Mock<IOutputPerSecondViewModelDataProvider>();
            var partialCircleSvgPathProvider = new Mock<IPartialCircleSvgPathProvider>();
            ILeaderboardViewModelFactory leaderboardViewModelFactory = null;

            // Act & Assert
            Assert.That(
                () => new LeaderboardService(outputPerSecondViewModelDataProvider.Object, partialCircleSvgPathProvider.Object, leaderboardViewModelFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILeaderboardViewModelFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange
            IOutputPerSecondViewModelDataProvider outputPerSecondViewModelDataProvider = null;
            IPartialCircleSvgPathProvider partialCircleSvgPathProvider = null;
            ILeaderboardViewModelFactory leaderboardViewModelFactory = null;

            // Act & Assert
            Assert.That(
                () => new LeaderboardService(outputPerSecondViewModelDataProvider, partialCircleSvgPathProvider, leaderboardViewModelFactory),
                Throws.InstanceOf<ArgumentNullException>());
        }
    }
}
