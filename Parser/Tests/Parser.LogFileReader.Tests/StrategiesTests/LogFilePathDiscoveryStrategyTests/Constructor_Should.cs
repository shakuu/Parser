using System;

using Moq;
using NUnit.Framework;

using Parser.LogFileReader.Contracts;
using Parser.LogFileReader.Strategies;

namespace Parser.LogFileReader.Tests.StrategiesTests.LogFilePathDiscoveryStrategyTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIEnvironmentFolderPathProviderParameterIsNull()
        {
            // Arrange
            IEnvironmentFolderPathProvider environmentFolderPathProvider = null;
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            // Act & Assert
            Assert.That(
                () => new LogFilePathDiscoveryStrategy(environmentFolderPathProvider, directoryFilesProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IEnvironmentFolderPathProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIDirectoryFilesProviderParameterIsNull()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            IDirectoryFilesProvider directoryFilesProvider = null;

            // Act & Assert
            Assert.That(
                () => new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IDirectoryFilesProvider)));
        }

        [Test]
        public void CreateValidInstance_WhenConstructorParametersAreValid()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            // Act
            var actualInstance = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ILogFilePathDiscoveryStrategy>());
        }
    }
}
