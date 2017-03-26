using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Utilities.Contracts;
using Parser.Data.Services.Contracts;
using Parser.MvcClient.Controllers;
using Parser.MvcClient.Controllers.Base;

namespace Parser.MvcClient.Tests.ControllersTests.LiveControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var liveService = new Mock<ILiveService>();
            var identityProvider = new Mock<IIdentityProvider>();

            // Act
            var actualInstance = new LiveController(liveService.Object, identityProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<LoggingController>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenILiveServiceParameterIsNull()
        {
            // Arrange
            ILiveService liveService = null;
            var identityProvider = new Mock<IIdentityProvider>();

            // Act & Assert
            Assert.That(
                () => new LiveController(liveService, identityProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILiveService)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIIdentityProviderParameterIsNull()
        {
            // Arrange
            var liveService = new Mock<ILiveService>();
            IIdentityProvider identityProvider = null;

            // Act & Assert
            Assert.That(
                () => new LiveController(liveService.Object, identityProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IIdentityProvider)));
        }
    }
}
