using System;

using Moq;
using NUnit.Framework;

using Parser.FileReader.Contracts;
using Parser.FileReader.Engines;
using Parser.FileReader.Factories;

namespace Parser.FileReader.Tests.EnginesTests.FileReaderEngineTests
{
    [TestFixture]
    public class Start_Should
    {
        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenLogFilePathParameterIsNullOrEmpty(string invalidLogFilePath)
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var actualInstance = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act & Assert
            Assert.That(
                () => actualInstance.Start(invalidLogFilePath),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("Invalid log file path."));
        }
    }
}
