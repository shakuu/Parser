using Moq;
using NUnit.Framework;

using TestStack.FluentMVCTesting;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.OwnerControllerTests
{
    [TestFixture]
    public class GetUsersOnPage_Should
    {
        [TestCase(0)]
        [TestCase(42)]
        [TestCase(23)]
        [TestCase(67)]
        public void InvokeIAuthOwnerService_GetAuthUsersOnPageOnceWithCorrectParameter(int pageNumber)
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            // Act
            ownerController.WithCallTo(c => c.GetUsersOnPage(pageNumber));

            // Assert
            authOwnerService.Verify(s => s.GetAuthUsersOnPage(pageNumber + 1), Times.Once);
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

            var expectedViewModel = new OwnerViewModel();
            authOwnerService.Setup(s => s.GetAuthUsersOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            ownerController
                .WithCallTo(c => c.GetUsersOnPage(pageNumber))
                .ShouldRenderPartialView("_AuthUserViewModelsPartial")
                .WithModel<OwnerViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
