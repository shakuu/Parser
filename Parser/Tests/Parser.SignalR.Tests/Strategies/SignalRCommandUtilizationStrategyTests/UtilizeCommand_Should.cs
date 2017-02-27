using System;

using Moq;
using NUnit.Framework;

using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Strategies;

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
    }
}
