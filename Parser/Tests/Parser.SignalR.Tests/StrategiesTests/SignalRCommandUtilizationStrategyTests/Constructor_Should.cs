using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Strategies;

namespace Parser.SignalR.Tests.StrategiesTests.SignalRCommandUtilizationStrategyTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateValidInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            // Assert
            Assert.That(signalRCommandUtilizationStrategy, Is.Not.Null.And.InstanceOf<ICommandUtilizationStrategy>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenISignalRHubConnectionServiceParameterIsNull()
        {
            // Arrange
            ISignalRHubConnectionService signalRHubConnectionService = null;
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            // Act & Assert
            Assert.That(
                () => new SignalRCommandUtilizationStrategy(signalRHubConnectionService, jsonConvertProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ISignalRHubConnectionService)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIJsonConvertProviderParameterIsNull()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            IJsonConvertProvider jsonConvertProvider = null;

            // Act & Assert
            Assert.That(
                () => new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IJsonConvertProvider)));
        }

        [Test]
        public void InvokeISignalRHubConnectionService_GetHubProxyProviderMethod_WithCorrectParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubName = "LogFileParserHub";

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            // Assert
            signalRHubConnectionService.Verify(s => s.GetHubProxyProvider(expectedHubName), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_OnMethod_WithCorrectHubMethodNameParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubMethodName = "UpdateParsingSessionId";

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            // Assert
            hubProxyProvider.Verify(p => p.On<string>(expectedHubMethodName, It.IsAny<Action<string>>()), Times.Once);
        }

        [Test]
        public void InvokeIHubProxyProvider_InvokeMethod_WithCorrectHubMethodNameParameter()
        {
            // Arrange
            var signalRHubConnectionService = new Mock<ISignalRHubConnectionService>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            signalRHubConnectionService.Setup(s => s.GetHubProxyProvider(It.IsAny<string>())).Returns(hubProxyProvider.Object);

            var expectedHubMethodName = "GetParsingSessionId";

            // Act
            var signalRCommandUtilizationStrategy = new SignalRCommandUtilizationStrategy(signalRHubConnectionService.Object, jsonConvertProvider.Object);

            // Assert
            hubProxyProvider.Verify(p => p.Invoke(expectedHubMethodName), Times.Once);
        }
    }
}
