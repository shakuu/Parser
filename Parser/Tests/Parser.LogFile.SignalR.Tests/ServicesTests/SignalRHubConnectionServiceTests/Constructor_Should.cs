using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Factories;
using Parser.LogFile.SignalR.Services;
using Parser.LogFile.SignalR.Tests.Mocks;

namespace Parser.LogFile.SignalR.Tests.ServicesTests.SignalRHubConnectionServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateValidInstance_WhenParametersAreValid()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            // Act
            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Assert
            Assert.That(signalRHubConnectionService, Is.Not.Null.And.InstanceOf<ISignalRHubConnectionService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIHubConnectionProviderFactoryParameterIsNull()
        {
            // Arrange
            IHubConnectionProviderFactory hubConnectionProviderFactory = null;
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            // Act & Assert
            Assert.That(
                () => new SignalRHubConnectionService(hubConnectionProviderFactory, hubProxyProviderFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IHubConnectionProviderFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIHubProxyProviderFactoryParameterIsNull()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            IHubProxyProviderFactory hubProxyProviderFactory = null;

            // Act & Assert
            Assert.That(
                () => new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IHubProxyProviderFactory)));
        }

        [Test]
        public void InvokeIHubConnectionProviderFactory_CreateHubConnectionProviderMethodOnceWithCorrectParameter()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            var hubConnectionUrl = "http://localhost:50800";

            // Act
            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Assert
            hubConnectionProviderFactory.Verify(f => f.CreateHubConnectionProvider(hubConnectionUrl), Times.Once);
        }

        [Test]
        public void InvokeIHubConnectionProvider_StartMethod()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            // Act
            var signalRHubConnectionService = new SignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);

            // Assert
            hubConnectionProvider.Verify(p => p.Start(), Times.Once);
        }

        [Test]
        public void InitializeHubProxyProvidersDictionary()
        {
            // Arrange
            var hubConnectionProviderFactory = new Mock<IHubConnectionProviderFactory>();
            var hubProxyProviderFactory = new Mock<IHubProxyProviderFactory>();

            var hubConnectionProvider = new Mock<IHubConnectionProvider>();
            hubConnectionProviderFactory.Setup(f => f.CreateHubConnectionProvider(It.IsAny<string>())).Returns(hubConnectionProvider.Object);

            // Act
            var signalRHubConnectionService = new MockSignalRHubConnectionService(hubConnectionProviderFactory.Object, hubProxyProviderFactory.Object);
            var actualHubProxyProvidersDictionaryValue = signalRHubConnectionService.HubProxyProviders;

            // Assert
            Assert.That(actualHubProxyProvidersDictionaryValue, Is.Not.Null.And.InstanceOf<IDictionary<string, IHubProxyProvider>>());
        }
    }
}
