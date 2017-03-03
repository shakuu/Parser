using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Providers;

namespace Parser.Common.Tests.ProvidersTests.CommandJsonConvertProviderTests
{
    [TestFixture]
    public class SerializeCommand_Should
    {
        [Test]
        public void ReturnNull_WhenICommandParameterIsNull()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            ICommand command = null;

            // Act
            var actualResult = commandJsonConvertProvider.SerializeCommand(command);

            // Assert
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public void InvokeIJsonConvertProvider_SerializeObjectMethodOnceWithCorrectParameter_WhenICommandIsValid()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            var command = new Mock<ICommand>();

            // Act
            commandJsonConvertProvider.SerializeCommand(command.Object);

            // Assert
            jsonConvertProvider.Verify(p => p.SerializeObject(command.Object), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenICommandIsValid()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var commandJsonConvertProvider = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            var command = new Mock<ICommand>();

            var expectedResult = "expected result";
            jsonConvertProvider.Setup(p => p.SerializeObject(It.IsAny<ICommand>())).Returns(expectedResult);

            // Act
            var actualResult = commandJsonConvertProvider.SerializeCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
