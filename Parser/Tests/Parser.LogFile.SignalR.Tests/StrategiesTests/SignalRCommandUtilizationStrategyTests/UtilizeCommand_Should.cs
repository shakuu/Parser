using System;

using Moq;
using NUnit.Framework;

using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Strategies;
using Parser.LogFile.SignalR.Tests.Mocks;

namespace Parser.LogFile.SignalR.Tests.StrategiesTests.SignalRCommandUtilizationStrategyTests
{
    [TestFixture]
    public class UtilizeCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICommandParameterIsNull()
        {
            // Arrange
            var commandUtilizationUpdateStrategy = new Mock<ICommandUtilizationUpdateStrategy>();
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(commandUtilizationUpdateStrategy.Object, signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

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
            var commandUtilizationUpdateStrategy = new Mock<ICommandUtilizationUpdateStrategy>();
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new MockSignalRCommandUtilizationStrategy(commandUtilizationUpdateStrategy.Object, signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);
            signalRCommandUtilizationStrategy.ParsingSessionId = "fake parsing session id";

            var command = new Mock<ICommand>();

            // Act
            signalRCommandUtilizationStrategy.UtilizeCommand(command.Object);

            // Assert
            commandJsonConvertProvider.Verify(p => p.SerializeCommand(command.Object), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_InvokeMethod_WithCorrectParameters()
        {
            // Arrange
            var commandUtilizationUpdateStrategy = new Mock<ICommandUtilizationUpdateStrategy>();
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var signalRCommandUtilizationStrategy = new MockSignalRCommandUtilizationStrategy(commandUtilizationUpdateStrategy.Object, signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

            var command = new Mock<ICommand>();

            var expectedHubMethodName = "SendCommand";
            var expectedParsingSessionId = Guid.NewGuid().ToString();
            var expectedSerializedCommand = "serialized command";

            commandJsonConvertProvider.Setup(p => p.SerializeCommand(It.IsAny<ICommand>())).Returns(expectedSerializedCommand);
            signalRCommandUtilizationStrategy.ParsingSessionId = expectedParsingSessionId;

            // Act
            signalRCommandUtilizationStrategy.UtilizeCommand(command.Object);

            // Assert
            hubProxyProvider.Verify(p => p.Invoke(expectedHubMethodName, expectedParsingSessionId, expectedSerializedCommand), Times.Once);
        }
    }
}
