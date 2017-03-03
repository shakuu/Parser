using System;

using Moq;
using NUnit.Framework;
using Parser.Common.Models;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Services;

namespace Parser.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
{
    [TestFixture]
    public class SendCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

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
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

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
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

            var deserializedCommand = new Mock<Command>();
            jsonConvertProvider.Setup(p => p.DeserializeObject<Command>(It.IsAny<string>())).Returns(deserializedCommand.Object);

            var engineId = "any engine id";
            var serializedCommand = "serialized command";

            // Act
            logFileParserHubService.SendCommand(engineId, serializedCommand);

            // Assert
            jsonConvertProvider.Verify(p => p.DeserializeObject<Command>(serializedCommand), Times.Once);
        }
    }
}
