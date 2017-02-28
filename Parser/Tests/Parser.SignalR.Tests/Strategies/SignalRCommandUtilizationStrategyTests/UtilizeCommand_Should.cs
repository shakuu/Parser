using System;

using Moq;
using NUnit.Framework;

using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Strategies;
using Parser.SignalR.Tests.Mocks;

namespace Parser.SignalR.Tests.Strategies.SignalRCommandUtilizationStrategyTests
{
    [TestFixture]
    public class UtilizeCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICommandParameterIsNull()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            ICommand invalidCommand = null;

            // Act & Assert
            Assert.That(
                () => signalRCommandUtilizationStrategy.UtilizeCommand(invalidCommand),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommand)));
        }

        [Test]
        public void InvokeIJsonConvertProvider_SerializeObjectMethod_WithCorrectParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            var command = new Mock<ICommand>();

            // Act
            signalRCommandUtilizationStrategy.UtilizeCommand(command.Object);

            // Assert
            jsonConvertProvider.Verify(p => p.SerializeObject(command.Object), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_InvokeMethod_WithCorrectParameters()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new MockSignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            var command = new Mock<ICommand>();

            var expectedHubMethodName = "SendCommand";
            var expectedParsingSessionId = Guid.NewGuid().ToString();
            var expectedSerializedCommand = "serialized command";

            jsonConvertProvider.Setup(p => p.SerializeObject(It.IsAny<object>())).Returns(expectedSerializedCommand);
            signalRCommandUtilizationStrategy.ParsingSessionId = expectedParsingSessionId;

            // Act
            signalRCommandUtilizationStrategy.UtilizeCommand(command.Object);

            // Assert
            hubProxyProvider.Verify(p => p.Invoke(expectedHubMethodName, expectedParsingSessionId, expectedSerializedCommand), Times.Once);
        }
    }
}
