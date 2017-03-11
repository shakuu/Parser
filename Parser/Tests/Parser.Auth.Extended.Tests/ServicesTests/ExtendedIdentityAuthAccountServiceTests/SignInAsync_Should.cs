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
    public class SignInAsync_Should
    {
        [Test]
        public async Task InvokeIIdentityAuthAccountService_SignInAsyncMethodOnceWithCorrectParameter()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();

            // Act
            await extendedIdentityAuthAccountService.SignInAsync(user);

            // Assert
            identityAuthAccountService.Verify(s => s.SignInAsync(user), Times.Once);
        }
    }
}
