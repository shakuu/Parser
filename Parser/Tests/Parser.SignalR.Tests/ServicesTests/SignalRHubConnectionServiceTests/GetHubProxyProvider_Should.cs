using System;

using Moq;
using NUnit.Framework;

using Parser.SignalR.Contracts;
using Parser.SignalR.Factories;
using Parser.SignalR.Services;
using Parser.SignalR.Tests.Mocks;

namespace Parser.SignalR.Tests.ServicesTests.SignalRHubConnectionServiceTests
{
    [TestFixture]
    public class GetHubProxyProvider_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenHubNameParameterIsNull()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            string hubName = null;

            // Act & Assert
            Assert.That(
                () => signalRHubConnectionService.GetHubProxyProvider(hubName),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(hubName)));
        }

        [Test]
        public void ThrowArgumentException_WhenHubNameParameterIsNull()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            var hubName = string.Empty;

            // Act & Assert
            Assert.That(
                () => signalRHubConnectionService.GetHubProxyProvider(hubName),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(hubName)));
        }

        [Test]
        public void ReturnCorrectIHubProxyProviderObject_WhenItAlreadyExistsInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var expectedIHubProxyProvider = new Mock<IHubProxyProvider>();
            var existingIHubProxyProviderHubName = "existing hubName";

            var signalRHubConnectionService = new MockSignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);
            signalRHubConnectionService.HubProxyProviders.Add(existingIHubProxyProviderHubName, expectedIHubProxyProvider.Object);

            // Act
            var actualIHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(existingIHubProxyProviderHubName);

            // Assert
            Assert.That(actualIHubProxyProvider, Is.SameAs(expectedIHubProxyProvider.Object));
        }
    }
}
