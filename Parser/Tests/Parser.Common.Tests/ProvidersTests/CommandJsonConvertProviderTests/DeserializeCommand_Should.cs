using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Models;
using Parser.Common.Providers;

namespace Parser.Common.Tests.ProvidersTests.CommandJsonConvertProviderTests
{
    [TestFixture]
    public class DeserializeCommand_Should
    {
        [Test]
        public void ReturnNull_WhenValueParameterIsNull()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            string value = null;

            // Act
            var actualResult = commandJsonConvertProvider.DeserializeCommand(value);

            // Assert
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public void InvokeIJsonConvertProvider_DeserializeObjectMethodOnceWithCorrectParameter_WhenICommandIsValid()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            var value = "any string";

            // Act
            commandJsonConvertProvider.DeserializeCommand(value);

            // Assert
            jsonConvertProvider.Verify(p => p.DeserializeObject<Command>(value), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenValueParameterIsValid()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            var value = "any string";

            var expectedResult = new Mock<Command>();
            jsonConvertProvider.Setup(p => p.DeserializeObject<Command>(It.IsAny<string>())).Returns(expectedResult.Object);

            // Act
            var actualResult = commandJsonConvertProvider.DeserializeCommand(value);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult.Object).And.InstanceOf<ICommand>());
        }
    }
}
