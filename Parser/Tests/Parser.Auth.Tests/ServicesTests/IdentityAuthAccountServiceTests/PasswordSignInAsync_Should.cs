using System;
using System.Threading.Tasks;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Services;

namespace Parser.Auth.Tests.ServicesTests.IdentityAuthAccountServiceTests
{
    [TestFixture]
    public class PasswordSignInAsync_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEmailParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            string email = null;
            var password = "any string";
            var rememberMe = true;

            // Act & Assert
            Assert.That(
               async () => await identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe),
               Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(email)));
        }

        [Test]
        public void ThrowArgumentException_WhenEmailParameterIsAnEmptyString()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var email = string.Empty;
            var password = "any string";
            var rememberMe = true;

            // Act & Assert
            Assert.That(
               async () => await identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe),
               Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(email)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPasswordParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var email = "any string";
            string password = null;
            var rememberMe = true;

            // Act & Assert
            Assert.That(
               async () => await identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe),
               Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(password)));
        }

        [Test]
        public void ThrowArgumentException_WhenPasswordParameterIsAnEmptyString()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var email = "any string";
            var password = string.Empty;
            var rememberMe = true;

            // Act & Assert
            Assert.That(
               async () => await identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe),
               Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(password)));
        }

        [Test]
        public async Task InvokeIAuthSignInManagerProvider_SignInManagerPropertyPasswordSignInAsyncMethodOnceWithCorrectParameters()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var email = "any string";
            var password = "any string";
            var rememberMe = true;

            var signInManager = new Mock<IAuthSignInManager>();
            authSignInManagerProvider.SetupGet(m => m.SignInManager).Returns(signInManager.Object);

            var shouldLockout = false;
            
            // Act
            await identityAuthAccountService.PasswordSignInAsync(email, password, rememberMe);

            // Assert
            signInManager.Verify(m => m.PasswordSignInAsync(email, password, rememberMe, shouldLockout), Times.Once);
        }
    }
}
