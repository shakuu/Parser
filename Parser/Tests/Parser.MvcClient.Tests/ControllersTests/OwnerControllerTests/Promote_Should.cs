using Moq;
using NUnit.Framework;

using TestStack.FluentMVCTesting;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.OwnerControllerTests
{
    [TestFixture]
    public class Promote_Should
    {
        [Test]
        public void InvokeIAuthOwnerService_AddRoleAdminMethodOnceWithCorrectParameter()
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            var username = "any string";
            var pageNumber = 0;

            // Act
            ownerController.WithCallTo(c => c.Promote(username, pageNumber));

            // Assert
            authOwnerService.Verify(s => s.AddRoleAdmin(username), Times.Once);
        }

        [Test]
        public void InvokeIAuthOwnerService_GetAuthUsersOnPageOnceWithCorrectParameter()
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            var username = "any string";
            var pageNumber = 0;

            // Act
            ownerController.WithCallTo(c => c.Promote(username, pageNumber));

            // Assert
            authOwnerService.Verify(s => s.GetAuthUsersOnPage(pageNumber), Times.Once);
        }

        [TestCase(0)]
        [TestCase(42)]
        [TestCase(23)]
        [TestCase(67)]
        public void RenderAuthUserViewModelsPartialViewWithCorrectViewModel(int pageNumber)
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            var username = "any string";

            var expectedViewModel = new OwnerViewModel();
            authOwnerService.Setup(s => s.GetAuthUsersOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            ownerController
                .WithCallTo(c => c.Promote(username, pageNumber))
                .ShouldRenderPartialView("_AuthUserViewModelsPartial")
                .WithModel<OwnerViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
