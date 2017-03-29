using Moq;
using NUnit.Framework;

using TestStack.FluentMVCTesting;

using Parser.Common.Constants.Configuration;
using Parser.Common.Utilities.Contracts;
using Parser.Data.ViewModels.AdminFab;
using Parser.Data.ViewModels.Factories;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.AdminFabControllerTests
{
    [TestFixture]
    public class DisplayAdminFab_Should
    {
        [Test]
        public void InvokeIIdentityProvider_IsOwnerMethodOnce()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            var adminFabController = new AdminFabController(identityProvider.Object, adminFabViewModelFactory.Object);

            identityProvider.Setup(p => p.IsOwner()).Returns(false);
            identityProvider.Setup(p => p.IsInRole(It.IsAny<string>())).Returns(false);

            // Act
            adminFabController
                .WithCallTo(c => c.DisplayAdminFab())
                .ShouldRenderPartialView("_AdminFabPartial");

            // Assert
            identityProvider.Verify(p => p.IsOwner(), Times.Once);
        }

        [Test]
        public void InvokeIIdentityProvider_IsInRoleMethodOnceWithCorrectParameter()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            var adminFabController = new AdminFabController(identityProvider.Object, adminFabViewModelFactory.Object);

            identityProvider.Setup(p => p.IsOwner()).Returns(false);
            identityProvider.Setup(p => p.IsInRole(It.IsAny<string>())).Returns(false);

            // Act
            adminFabController
                .WithCallTo(c => c.DisplayAdminFab())
                .ShouldRenderPartialView("_AdminFabPartial");

            // Assert
            identityProvider.Verify(p => p.IsInRole(UserRoles.AdminRole), Times.Once);
        }

        [Test]
        public void InvokeIAdminFabViewModelFactory_CreateAdminFabViewModelOnceWithCorrectParameters()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            var adminFabController = new AdminFabController(identityProvider.Object, adminFabViewModelFactory.Object);

            identityProvider.Setup(p => p.IsOwner()).Returns(false);
            identityProvider.Setup(p => p.IsInRole(It.IsAny<string>())).Returns(false);

            // Act
            adminFabController
                .WithCallTo(c => c.DisplayAdminFab())
                .ShouldRenderPartialView("_AdminFabPartial");

            // Assert
            adminFabViewModelFactory.Verify(f => f.CreateAdminFabViewModel(false, false), Times.Once);
        }

        [Test]
        public void RenderCorrectPartialViewWithCorrectModel()
        {
            // Arrange
            var identityProvider = new Mock<IIdentityProvider>();
            var adminFabViewModelFactory = new Mock<IAdminFabViewModelFactory>();

            var adminFabController = new AdminFabController(identityProvider.Object, adminFabViewModelFactory.Object);

            identityProvider.Setup(p => p.IsOwner()).Returns(false);
            identityProvider.Setup(p => p.IsInRole(It.IsAny<string>())).Returns(false);

            var expectedViewModel = new AdminFabViewModel(false, false);
            adminFabViewModelFactory.Setup(f => f.CreateAdminFabViewModel(It.IsAny<bool>(), It.IsAny<bool>())).Returns(expectedViewModel);

            // Act & Assert
            adminFabController
                .WithCallTo(c => c.DisplayAdminFab())
                .ShouldRenderPartialView("_AdminFabPartial")
                .WithModel<AdminFabViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
