using System;

using Moq;
using NUnit.Framework;

using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Strategies;

namespace Parser.LogFile.SignalR.Tests.StrategiesTests.SignalRCommandUtilizationStrategyTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateValidInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

            // Assert
            Assert.That(signalRCommandUtilizationStrategy, Is.Not.Null.And.InstanceOf<ICommandUtilizationStrategy>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenISignalRHubConnectionServiceParameterIsNull()
        {
            // Arrange
            ISignalRHubConnectionService signalRHubConnectionService = null;
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            // Act & Assert
            Assert.That(
                () => new SignalRCommandUtilizationStrategy(signalRHubConnectionService, commandJsonConvertProvider.Object, remoteUserProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ISignalRHubConnectionService)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIJsonConvertProviderParameterIsNull()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            ICommandJsonConvertProvider commandJsonConvertProvider = null;
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            // Act & Assert
            Assert.That(
                () => new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, commandJsonConvertProvider, remoteUserProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IJsonConvertProvider)));
        }

        [Test]
        public void InvokeISignalRHubConnectionService_GetHubProxyProviderMethod_WithCorrectParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubName = "LogFileParserHub";

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

            // Assert
            signalRHubConnectionService.Verify(s => s.GetHubProxyProvider(expectedHubName), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_OnMethod_WithCorrectHubMethodNameParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubMethodName = "UpdateParsingSessionId";

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

            // Assert
            hubProxyProvider.Verify(p => p.On<string>(expectedHubMethodName, It.IsAny<Action<string>>()), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_InvokeMethod_WithCorrectHubMethodNameParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();
            var remoteUserProvider = new Mock<IRemoteUserProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubMethodName = "GetParsingSessionId";
            string expectedUsername = null;

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, commandJsonConvertProvider.Object, remoteUserProvider.Object);

            // Assert
            hubProxyProvider.Verify(p => p.Invoke(expectedHubMethodName, expectedUsername), Times.Once);
        }
    }
}
