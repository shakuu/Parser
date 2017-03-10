using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Services;

namespace Parser.Auth.Tests.ServicesTests.IdentityAuthAccountServiceTests
{
    [TestFixture]
    public class CreateAsync_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAuthUserParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            AuthUser user = null;
            var password = "any string";

            // Act & Assert
            Assert.That(
                async () => await identityAuthAccountService.CreateAsync(user, password),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(AuthUser)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPasswordParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var user = new AuthUser();
            string password = null;

            // Act & Assert
            Assert.That(
                async () => await identityAuthAccountService.CreateAsync(user, password),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(password)));
        }

        [Test]
        public void ThrowArgumentException_WhenPasswordParameterIsAnEmptyString()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var user = new AuthUser();
            var password = string.Empty;

            // Act & Assert
            Assert.That(
                async () => await identityAuthAccountService.CreateAsync(user, password),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(password)));
        }

        [Test]
        public async Task InvokeIAuthUserManagerProvider_UserManagerPropertyCreateAsyncMethodOnceWithCorrectParameters()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var user = new AuthUser();
            var password = "any string";

            var userManager = new Mock<IAuthUserManager>();
            authUserManagerProvider.SetupGet(p => p.UserManager).Returns(userManager.Object);

            // Act
            await identityAuthAccountService.CreateAsync(user, password);

            // Assert
            userManager.Verify(m => m.CreateAsync(user, password), Times.Once);
        }
    }
}
