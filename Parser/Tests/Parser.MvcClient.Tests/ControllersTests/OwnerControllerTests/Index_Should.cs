using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;
using Parser.MvcClient.Controllers;
using TestStack.FluentMVCTesting;

namespace Parser.MvcClient.Tests.ControllersTests.OwnerControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void InvokeIAuthOwnerService_GetAuthUsersOnPageMethodOnceWithCorrectParameter()
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            // Act
            ownerController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();

            // Assert
            authOwnerService.Verify(s => s.GetAuthUsersOnPage(1), Times.Once);
        }

        [Test]
        public void RenderCorrectViewWithCorrectViewModel()
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            var ownerController = new OwnerController(authOwnerService.Object);

            var expectedViewModel = new OwnerViewModel();
            authOwnerService.Setup(s => s.GetAuthUsersOnPage(It.IsAny<int>())).Returns(expectedViewModel);

            // Act & Assert
            ownerController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<OwnerViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
