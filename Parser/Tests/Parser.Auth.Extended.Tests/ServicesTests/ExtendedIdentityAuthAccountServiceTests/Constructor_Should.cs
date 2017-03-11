using System;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Contracts;
using Parser.Auth.Extended.Services;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;

namespace Parser.Auth.Extended.Tests.ServicesTests.ExtendedIdentityAuthAccountServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIExtendedIdentityAuthAccountServiceInstance_WhenParametersAreValid()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            // Act
            var actualInstance = new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IIdentityAuthAccountService>());
            Assert.That(actualInstance, Is.InstanceOf<IExtendedIdentityAuthAccountService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIIdentityAuthAccountServiceParameterIsNull()
        {
            // Arrange
            IIdentityAuthAccountService identityAuthAccountService = null;
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            var createParserUserService = new Mock<ICreateParserUserService>();

            // Act & Assert
            Assert.That(
                () => new ExtendedIdentityAuthAccountService(identityAuthAccountService, registerParserUserViewModelFactory.Object, createParserUserService.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IIdentityAuthAccountService)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIRegisterParserUserViewModelFactoryParameterIsNull()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            IRegisterParserUserViewModelFactory registerParserUserViewModelFactory = null;
            var createParserUserService = new Mock<ICreateParserUserService>();

            // Act & Assert
            Assert.That(
                () => new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory, createParserUserService.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IRegisterParserUserViewModelFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICreateParserUserServiceParameterIsNull()
        {
            // Arrange
            var identityAuthAccountService = new Mock<IIdentityAuthAccountService>();
            var registerParserUserViewModelFactory = new Mock<IRegisterParserUserViewModelFactory>();
            ICreateParserUserService createParserUserService = null;

            // Act & Assert
            Assert.That(
                () => new ExtendedIdentityAuthAccountService(identityAuthAccountService.Object, registerParserUserViewModelFactory.Object, createParserUserService),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICreateParserUserService)));
        }
    }
}
