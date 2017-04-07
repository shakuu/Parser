using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.SignalR.Services;

namespace Parser.LogFile.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
{
    [TestFixture]
    public class SendCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandEnumerationJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            string engineId = null;
            var serializedCommand = "serialized command";

            // Act & Assert
            Assert.That(
                () => logFileParserHubService.SendCommand(engineId, serializedCommand),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void ThrowArgumentException_WhenEngineIdParameterIsEmpty()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandEnumerationJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var engineId = string.Empty;
            var serializedCommand = "serialized command";

            // Act & Assert
            Assert.That(
                () => logFileParserHubService.SendCommand(engineId, serializedCommand),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void InvokeIJsonConvertProvider_DeserializeObjectOnceWithCorrectParameter()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandEnumerationJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var deserializedCommand = new Mock<ICommand>();
            commandJsonConvertProvider.Setup(p => p.DeserializeCommand(It.IsAny<string>())).Returns(deserializedCommand.Object);

            var engineId = "any engine id";
            var serializedCommand = "serialized command";

            // Act
            logFileParserHubService.SendCommand(engineId, serializedCommand);

            // Assert
            commandJsonConvertProvider.Verify(p => p.DeserializeCommand(serializedCommand), Times.Once);
        }

        [Test]
        public void ThrowArgumentException_WhenICommandJsonConvertProviderIsUnableToDeserializeInput()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandEnumerationJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var deserializedCommand = new Mock<ICommand>();
            commandJsonConvertProvider.Setup(p => p.DeserializeCommand(It.IsAny<string>())).Returns<ICommand>(null);

            var engineId = "any engine id";
            string serializedCommand = null;

            // Act & Assert
            Assert.That(
                () => logFileParserHubService.SendCommand(engineId, serializedCommand),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(serializedCommand)));
        }

        [Test]
        public void InvokeILogFileParserEngineManager_EnqueueCommandToEngineWithIdMethodOnceWithCorrectParameters()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandEnumerationJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var deserializedCommand = new Mock<ICommand>();
            commandJsonConvertProvider.Setup(p => p.DeserializeCommand(It.IsAny<string>())).Returns(deserializedCommand.Object);

            var engineId = "any engine id";
            var serializedCommand = "serialized command";

            // Act
            logFileParserHubService.SendCommand(engineId, serializedCommand);

            // Assert
            logFileParserEngineManager.Verify(m => m.EnqueueCommandToEngineWithId(engineId, deserializedCommand.Object), Times.Once);
        }
    }
}
