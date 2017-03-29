using System;

using Microsoft.AspNet.SignalR.Client;

using Moq;
using NUnit.Framework;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Factories;
using Parser.LogFile.SignalR.Services;
using Parser.LogFile.SignalR.Tests.Mocks;

namespace Parser.LogFile.SignalR.Tests.ServicesTests.SignalRHubConnectionServiceTests
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
        public void ReturnCorrectIHubProxyProviderObject_WhenRequestedHubNameAlreadyExistsInHubProxyProvidersDictionary()
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

        [Test]
        public void InvokeIHubConnectionProvider_StopMethodOnce_WhenRequestedHubNameDoesNotExistInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var unexistingIHubProxyProviderHubName = "unexisting hubName";

            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Act
            signalRHubConnectionService.GetHubProxyProvider(unexistingIHubProxyProviderHubName);

            // Assert
            hubConnectionProvider.Verify(p => p.Stop(), Times.Once);
        }

        [Test]
        public void InvokeIHubConnectionProvider_CreateHubProxyMethodOnceWithCorrectParameter_WhenRequestedHubNameDoesNotExistInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var unexistingIHubProxyProviderHubName = "unexisting hubName";

            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Act
            signalRHubConnectionService.GetHubProxyProvider(unexistingIHubProxyProviderHubName);

            // Assert
            hubConnectionProvider.Verify(p => p.CreateHubProxy(unexistingIHubProxyProviderHubName), Times.Once);
        }

        [Test]
        public void InvokeIHubConnectionProviderFactory_CreateHubProxyProviderMethodOnceWithCorrectParameter_WhenRequestedHubNameDoesNotExistInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var hubProxy = new Mock<IHubProxy>();
            hubConnectionProvider.Setup(p => p.CreateHubProxy(It.IsAny<string>())).Returns(hubProxy.Object);

            var unexistingIHubProxyProviderHubName = "unexisting hubName";

            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Act
            signalRHubConnectionService.GetHubProxyProvider(unexistingIHubProxyProviderHubName);

            // Assert
            hubProxyProviderFactory.Verify(f => f.CreateHubProxyProvider(hubProxy.Object), Times.Once);
        }

        [Test]
        public void AddANewlyCreatedIHubProxyProvider_WithCorrectKey_WhenRequestedHubNameDoesNotExistInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var hubProxy = new Mock<IHubProxy>();
            hubConnectionProvider.Setup(p => p.CreateHubProxy(It.IsAny<string>())).Returns(hubProxy.Object);

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            hubProxyProviderFactory.Setup(f => f.CreateHubProxyProvider(It.IsAny<IHubProxy>())).Returns(hubProxyProvider.Object);

            var unexistingIHubProxyProviderHubName = "unexisting hubName";

            var signalRHubConnectionService = new MockSignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Act
            signalRHubConnectionService.GetHubProxyProvider(unexistingIHubProxyProviderHubName);
            var hubProxyProvidersDictionaryContainsCorrectKey = signalRHubConnectionService.HubProxyProviders.ContainsKey(unexistingIHubProxyProviderHubName);
            var actualHubProxyProvidersDictionaryValue = signalRHubConnectionService.HubProxyProviders[unexistingIHubProxyProviderHubName];
            var expectedHubProxyProvidersDictionaryValue = hubProxyProvider.Object;

            // Assert
            Assert.That(hubProxyProvidersDictionaryContainsCorrectKey, Is.True);
            Assert.That(actualHubProxyProvidersDictionaryValue, Is.SameAs(expectedHubProxyProvidersDictionaryValue));
        }

        [Test]
        public void ReturnCorrectIHubProxyProviderObject_WhenRequestedHubNameDoesNotExistInHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var hubProxy = new Mock<IHubProxy>();
            hubConnectionProvider.Setup(p => p.CreateHubProxy(It.IsAny<string>())).Returns(hubProxy.Object);

            var hubProxyProvider = new Mock<IHubProxyProvider>();
            hubProxyProviderFactory.Setup(f => f.CreateHubProxyProvider(It.IsAny<IHubProxy>())).Returns(hubProxyProvider.Object);

            var unexistingIHubProxyProviderHubName = "unexisting hubName";

            var signalRHubConnectionService = new MockSignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Act
            var expectedHubProxyProvider = hubProxyProvider.Object;
            var actualHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(unexistingIHubProxyProviderHubName);

            // Assert
            Assert.That(actualHubProxyProvider, Is.SameAs(expectedHubProxyProvider));
        }
    }
}
