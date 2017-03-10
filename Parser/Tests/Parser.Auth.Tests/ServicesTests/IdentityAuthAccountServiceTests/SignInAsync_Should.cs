using System;
using System.Threading.Tasks;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Services;

namespace Parser.Auth.Tests.ServicesTests.IdentityAuthAccountServiceTests
{
    [TestFixture]
    public class SignInAsync_Should
    {
        [Test]
        public void ThrowArgumentNulLException_WhenAuthUserParameterIsNull()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            AuthUser user = null;

            // Act & Assert
            Assert.That(
               async () => await identityAuthAccountService.SignInAsync(user),
               Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(AuthUser)));
        }

        [Test]
        public async Task InvokeIAuthSignInManagerProvider_SignInManagerPropertySignInAsyncMethodOnceWithCorrectParameters()
        {
            // Arrange
            var authSignInManagerProvider = new Mock<IAuthSignInManagerProvider>();
            var authUserManagerProvider = new Mock<IAuthUserManagerProvider>();

            var identityAuthAccountService = new IdentityAuthAccountService(authSignInManagerProvider.Object, authUserManagerProvider.Object);

            var user = new AuthUser();

            var signInManager = new Mock<IAuthSignInManager>();
            authSignInManagerProvider.SetupGet(m => m.SignInManager).Returns(signInManager.Object);

            var isPersistent = false;
            var rememberBrowser = false;

            // Act
            await identityAuthAccountService.SignInAsync(user);

            // Assert
            signInManager.Verify(m => m.SignInAsync(user, isPersistent, rememberBrowser), Times.Once);
        }
    }
}
