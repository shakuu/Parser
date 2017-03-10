using System;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Services;

namespace Parser.Auth.Tests.ServicesTests.IdentityAuthAccountServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIIdentityAuthAccountServiceInstance_WhenParametersAreValid()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            // Act
            var actualInstance = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IIdentityAuthAccountService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAuthSignInManagerProviderParameterIsNull()
        {
            // Arrange
            IAuthSignInManagerProvider authSignInManagerProvider = null;
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            // Act & Assert
            Assert.That(
                () => new IdentityAuthAccountService(authSignInManagerProvider, authUserManagerProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IAuthSignInManagerProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAuthUserManagerProviderParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            IAuthUserManagerProvider authUserManagerProvider = null;

            // Act & Assert
            Assert.That(
                () => new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IAuthUserManagerProvider)));
        }
    }
}
