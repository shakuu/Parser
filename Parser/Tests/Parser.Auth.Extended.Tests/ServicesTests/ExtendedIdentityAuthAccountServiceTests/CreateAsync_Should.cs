using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Services;
using Parser.Auth.Extended.Tests.Mocks;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels;
using Parser.Data.ViewModels.Factories;

namespace Parser.Auth.Extended.Tests.ServicesTests.ExtendedIdentityAuthAccountServiceTests
{
    [TestFixture]
    public class CreateAsync_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAuthUserParameterIsNull()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            AuthUser user = null;
            var password = "any string";

            // Act & Assert
            Assert.That(
                async () => await extendedIdentityAuthAccountService.CreateAsync(user, password),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(AuthUser)));
        }

        [Test]
        public async Task InvokeIIdentityAuthAccountService_CreateAsyncMethodOnceWithCorrectParameters()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            var password = "any string";

            var identityResult = new MockIdentityResult(false);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => identityResult));

            // Act
            await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            identityAuthAccountService.Verify(s => s.CreateAsync(user, password), Times.Once);
        }

        [Test]
        public async Task InvokeIRegisterParserUserViewModelFactory_CreateRegisterParserUserViewModelOnceWithCorrectParameter_IfResultSucceededIsTrue()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            user.UserName = "any string";
            var password = "any string";

            var identityResult = new MockIdentityResult(true);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => identityResult));

            // Act
            await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            registerParserUserViewModelFactory.Verify(f => f.CreateRegisterParserUserViewModel(user.UserName), Times.Once);
        }

        [Test]
        public async Task NotInvokeIRegisterParserUserViewModelFactory_CreateRegisterParserUserViewModelOnceWithCorrectParameter_IfResultSucceededIsFalse()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            user.UserName = "any string";
            var password = "any string";

            var identityResult = new MockIdentityResult(false);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => identityResult));

            // Act
            await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            registerParserUserViewModelFactory.Verify(f => f.CreateRegisterParserUserViewModel(user.UserName), Times.Never);
        }

        [Test]
        public async Task InvokeICreateParserUserService_CreateParserUserOnceWithCorrectParameter_IfResultSucceededIsTrue()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            user.UserName = "any string";
            var password = "any string";

            var identityResult = new MockIdentityResult(true);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => identityResult));

            var registerParserUserViewModel = new RegisterParserUserViewModel("any string");
            registerParserUserViewModelFactory.Setup(f => f.CreateRegisterParserUserViewModel(It.IsAny<string>())).Returns(registerParserUserViewModel);

            // Act
            await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            createParserUserService.Verify(s => s.CreateParserUser(registerParserUserViewModel), Times.Once);
        }

        [Test]
        public async Task NotInvokeICreateParserUserService_CreateParserUserOnceWithCorrectParameter_IfResultSucceededIsFalse()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            user.UserName = "any string";
            var password = "any string";

            var identityResult = new MockIdentityResult(false);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => identityResult));

            var registerParserUserViewModel = new RegisterParserUserViewModel("any string");
            registerParserUserViewModelFactory.Setup(f => f.CreateRegisterParserUserViewModel(It.IsAny<string>())).Returns(registerParserUserViewModel);

            // Act
            await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            createParserUserService.Verify(s => s.CreateParserUser(registerParserUserViewModel), Times.Never);
        }

        [Test]
        public async Task ReturnCorrectIdentityResultInstance()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            var extendedIdentityAuthAccountService = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            var user = new AuthUser();
            var password = "any string";

            var expectedReturnedIdentityResultInstance = new MockIdentityResult(false);
            identityAuthAccountService.Setup(s => s.CreateAsync(It.IsAny<AuthUser>(), It.IsAny<string>())).Returns(Task.Run<IdentityResult>(() => expectedReturnedIdentityResultInstance));

            // Act
            var actualReturnedIdentityResultInstance = await extendedIdentityAuthAccountService.CreateAsync(user, password);

            // Assert
            Assert.That(actualReturnedIdentityResultInstance, Is.SameAs(expectedReturnedIdentityResultInstance));
        }
    }
}
