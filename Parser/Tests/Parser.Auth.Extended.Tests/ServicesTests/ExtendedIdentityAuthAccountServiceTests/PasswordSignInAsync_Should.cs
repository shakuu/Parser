using System.Threading.Tasks;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Services;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.Auth.Extended.Tests.ServicesTests.ExtendedIdentityAuthAccountServiceTests
{
    [TestFixture]
    public class PasswordSignInAsync_Should
    {
        [Test]
        public async Task InvokeIIdentityAuthAccountService_PasswordSignInAsyncOnceWithCorrectParameters()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var email = "any string";
            var password = "any string";
            var rememberMe = true;

            // Act
            await extendedIdentityAuthAccountService.PasswordSignInAsync(email, password, rememberMe);

            // Assert
            identityAuthAccountService.Verify(s => s.PasswordSignInAsync(email, password, rememberMe), Times.Once);
        }
    }
}
