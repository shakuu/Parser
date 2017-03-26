using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Parser.Auth.Contracts;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.Tests.ControllersTests.OwnerControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var authOwnerService = new Mock<IAuthOwnerService>();

            // Act
            var actualInstance = new OwnerController(authOwnerService.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<Controller>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAuthOwnerServiceParameterIsNull()
        {
            // Arrange
            IAuthOwnerService authOwnerService = null;

            // Act & Assert
            Assert.That(
                () => new OwnerController(authOwnerService),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IAuthOwnerService)));
        }
    }
}
