using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Parser.LogFileReader.Contracts;
using Parser.LogFileReader.Strategies;

namespace Parser.LogFileReader.Tests.StrategiesTests.LogFilePathDiscoveryStrategyTests
{
    [TestFixture]
    public class DiscoverLogFile_Should
    {
        [Test]
        public void InvokeIEnvironmentFolderPathProvider_GetEnvironmentFolderPath_OnceWithCorrectParameter()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            var logFilePathDiscoveryStrategy = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            var myDocumentsFolderName = "MyDocuments";

            // Act
            logFilePathDiscoveryStrategy.DiscoverLogFile();

            // Assert
            environmentFolderPathProvider.Verify(p => p.GetEnvironmentFolderPath(myDocumentsFolderName), Times.Once());
        }

        [Test]
        public void InvokeIDirectoryFilesProvider_GetDirectoryFiles_OnceWithCorrectParameter()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            var logFilePathDiscoveryStrategy = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            var defaultCombatLogsPath = "\\Star Wars - The Old Republic\\CombatLogs";
            var environmentFolderPathProviderReturnValue = "C:\\MyDocuments";

            environmentFolderPathProvider.Setup(p => p.GetEnvironmentFolderPath(It.IsAny<string>())).Returns(environmentFolderPathProviderReturnValue);

            var expectedGetDirectoryFilesParameter = environmentFolderPathProviderReturnValue + defaultCombatLogsPath;

            // Act
            logFilePathDiscoveryStrategy.DiscoverLogFile();

            // Assert
            directoryFilesProvider.Verify(p => p.GetDirectoryFiles(expectedGetDirectoryFilesParameter), Times.Once());
        }

        [Test]
        public void ReturnNull_WhenIDirectoryFilesProviderReturnsNull()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            var logFilePathDiscoveryStrategy = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            IEnumerable<string> directoryFilesProviderReturnValue = null;
            directoryFilesProvider.Setup(p => p.GetDirectoryFiles(It.IsAny<string>())).Returns(directoryFilesProviderReturnValue);

            // Act
            var actualResult = logFilePathDiscoveryStrategy.DiscoverLogFile();

            // Assert
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public void ReturnNull_WhenIDirectoryFilesProviderReturnsEmptyCollection()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            var logFilePathDiscoveryStrategy = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            IEnumerable<string> directoryFilesProviderReturnValue = new List<string>();
            directoryFilesProvider.Setup(p => p.GetDirectoryFiles(It.IsAny<string>())).Returns(directoryFilesProviderReturnValue);

            // Act
            var actualResult = logFilePathDiscoveryStrategy.DiscoverLogFile();

            // Assert
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public void ReturnTheLastElement_WhenIDirectoryFilesProviderReturnsAValidCollection()
        {
            // Arrange
            var environmentFolderPathProvider = new Mock<IEnvironmentFolderPathProvider>();
            var directoryFilesProvider = new Mock<IDirectoryFilesProvider>();

            var logFilePathDiscoveryStrategy = new LogFilePathDiscoveryStrategy(environmentFolderPathProvider.Object, directoryFilesProvider.Object);

            var expectedlogFilePathDiscoveryStrategyReturnValue = "4";
            IEnumerable<string> directoryFilesProviderReturnValue = new List<string>()
            {
                "1",
                "2",
                "3",
                expectedlogFilePathDiscoveryStrategyReturnValue
            };

            directoryFilesProvider.Setup(p => p.GetDirectoryFiles(It.IsAny<string>())).Returns(directoryFilesProviderReturnValue);

            // Act
            var actualResult = logFilePathDiscoveryStrategy.DiscoverLogFile();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedlogFilePathDiscoveryStrategyReturnValue));
        }
    }
}
