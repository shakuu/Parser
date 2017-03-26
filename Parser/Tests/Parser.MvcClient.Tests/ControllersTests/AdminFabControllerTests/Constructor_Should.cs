using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Parser.Common.Utilities.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.AdminFabControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            // Act
            var actualInstance = new AdminFabController(identityProvider.Object, adminFabViewModelFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<Controller>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIIdentityProviderParameterIsNull()
        {
            // Arrange
            IIdentityProvider identityProvider = null;
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            // Act & Assert
            Assert.That(
                () => new AdminFabController(identityProvider, adminFabViewModelFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IIdentityProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAdminFabViewModelFactoryParameterIsNull()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            IAdminFabViewModelFactory adminFabViewModelFactory = null;

            // Act & Assert
            Assert.That(
                () => new AdminFabController(identityProvider.Object, adminFabViewModelFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IAdminFabViewModelFactory)));
        }
    }
}
