using System;

using Moq;
using NUnit.Framework;

using Parser.LogFileReader.Contracts;
using Parser.LogFileReader.Engines;
using Parser.LogFileReader.Factories;

namespace Parser.LogFileReader.Tests.EnginesTests.FileReaderEngineTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateValidObject_WhenAllParametersAreValid()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act 
            var actualInstance = new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ILogFileReaderEngine>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICommandParsingStrategyIsNull()
        {
            // Arrange
            ICommandParsingStrategy commandParsingStrategy = null;
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommandParsingStrategy)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICommandUtilizationStrategyIsNull()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            ICommandUtilizationStrategy commandUtilizationStrategy = null;
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommandUtilizationStrategy)));
        }

        public void ThrowArgumentNullException_WhenILogFilePathDiscoveryStrategyIsNull()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            ILogFilePathDiscoveryStrategy logFilePathDiscoveryStrategy = null;
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILogFilePathDiscoveryStrategy)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIFileReaderAutoResetEventFactoryIsNull()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            IFileReaderAutoResetEventFactory fileReaderAutoResetEventFactory = null;
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IFileReaderAutoResetEventFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIFileReaderFileSystemWatcherFactoryIsNull()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            IFileReaderFileSystemWatcherFactory fileReaderFileSystemWatcherFactory = null;
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory, fileReaderInputProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IFileReaderFileSystemWatcherFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIFileReaderInputProviderFactoryIsNull()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            IFileReaderInputProviderFactory fileReaderInputProviderFactory = null;

            // Act & Assert
            Assert.That(
                () => new LogFileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IFileReaderInputProviderFactory)));
        }
    }
}
