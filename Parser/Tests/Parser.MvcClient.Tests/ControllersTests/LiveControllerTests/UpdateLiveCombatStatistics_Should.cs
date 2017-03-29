using Moq;
using NUnit.Framework;

using TestStack.FluentMVCTesting;

using Parser.Common.Utilities.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Live;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.LiveControllerTests
{
    [TestFixture]
    public class UpdateLiveCombatStatistics_Should
    {
        [Test]
        public void InvokeIIdentityProvider_GetUsernameMethodOnce()
        {
            // Arrange
            var liveService = new Mock<ILiveService>();
            var identityProvider = new Mock<IIdentityProvider>();

            var liveController = new LiveController(liveService.Object, identityProvider.Object);

            // Act
            liveController
                .WithCallTo(c => c.UpdateLiveCombatStatistics())
                .ShouldRenderPartialView("_LiveStatisticsViewModel");

            // Assert
            identityProvider.Verify(p => p.GetUsername(), Times.Once);
        }

        [Test]
        public void InvokeILiveService_GetLiveStatisticsViewModelMethodOnceWithCorrectParameter()
        {
            // Arrange
            var liveService = new Mock<ILiveService>();
            var identityProvider = new Mock<IIdentityProvider>();

            var liveController = new LiveController(liveService.Object, identityProvider.Object);

            var username = "any string";
            identityProvider.Setup(p => p.GetUsername()).Returns(username);

            // Act
            liveController
                .WithCallTo(c => c.UpdateLiveCombatStatistics())
                .ShouldRenderPartialView("_LiveStatisticsViewModel");

            // Assert
            liveService.Verify(s => s.GetLiveStatisticsViewModel(username), Times.Once);
        }

        [Test]
        public void RenderCorrectViewWithCorrectViewModel()
        {
            // Arrange
            var liveService = new Mock<ILiveService>();
            var identityProvider = new Mock<IIdentityProvider>();

            var liveController = new LiveController(liveService.Object, identityProvider.Object);

            var expectedViewModel = new LiveStatisticsViewModel();
            liveService.Setup(s => s.GetLiveStatisticsViewModel(It.IsAny<string>())).Returns(expectedViewModel);

            // Act & Assert
            liveController
                .WithCallTo(c => c.UpdateLiveCombatStatistics())
                .ShouldRenderPartialView("_LiveStatisticsViewModel")
                .WithModel<LiveStatisticsViewModel>(actualViewModel =>
                {
                    Assert.That(actualViewModel, Is.SameAs(expectedViewModel));
                });
        }
    }
}
