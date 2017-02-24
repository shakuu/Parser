using System;
using System.Threading;
using System.Threading.Tasks;

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
        [Test]
        public void ThrowArgumentNullException_WhenLogFilePathParameterIsNullOrEmpty()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var actualInstance = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            string invalidLogFilePath = null;

            // Act & Assert
            Assert.That(
                () => actualInstance.Start(invalidLogFilePath),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("Invalid log file path."));
        }

        [Test]
        public void ThrowArgumentException_WhenLogFilePathParameterIsNullOrEmpty()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var actualInstance = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            string invalidLogFilePath = string.Empty;

            // Act & Assert
            Assert.That(
                () => actualInstance.Start(invalidLogFilePath),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains("Invalid log file path."));
        }

        [Test]
        public void InvokeIFileReaderAutoResetEventFactory_CreateFileReaderAutoResetEventMethod_OnceWithCorrectArgument()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var fileReaderInputProviderMock = new Mock<IFileReaderInputProvider>();
            fileReaderInputProviderMock.Setup(i => i.ReadLine()).Returns<string>(null);
            fileReaderInputProviderFactory.Setup(f => f.CreateFileReaderInputProvider(It.IsAny<string>())).Returns(fileReaderInputProviderMock.Object);

            var fileReaderAutoResetEventMock = new Mock<IFileReaderAutoResetEvent>();
            fileReaderAutoResetEventMock.Setup(e => e.WaitOne(It.IsAny<int>())).Returns(true);
            fileReaderAutoResetEventFactory.Setup(f => f.CreateFileReaderAutoResetEvent(It.IsAny<bool>())).Returns(fileReaderAutoResetEventMock.Object);

            var logFilePath = "Fake Log Path";

            var actualInstance = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act
            Task.Run(() => actualInstance.Start(logFilePath));

            Thread.Sleep(50);
            actualInstance.Stop();

            // Assert
            fileReaderAutoResetEventFactory.Verify(f => f.CreateFileReaderAutoResetEvent(false), Times.Once());
        }
    }
}
