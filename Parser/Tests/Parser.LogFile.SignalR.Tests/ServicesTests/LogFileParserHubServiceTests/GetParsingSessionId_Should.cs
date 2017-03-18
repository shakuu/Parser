using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.SignalR.Services;

namespace Parser.LogFile.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
{
    [TestFixture]
    public class GetParsingSessionId_Should
    {
        [Test]
        public void ShouldInvokeILogFileParserEngineManager_StartNewLogFileParserEngineMethodOnce()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var username = "any username";

            // Act
            logFileParserHubService.GetParsingSessionId(username);

            // Assert
            logFileParserEngineManager.Verify(m => m.StartLogFileParserEngine(username), Times.Once);
        }

        [Test]
        public void ShouldReturnCorrectStringValue()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var expectedReturnValue = "expected return value";
            logFileParserEngineManager.Setup(m => m.StartLogFileParserEngine(It.IsAny<string>())).Returns(expectedReturnValue);

            var username = "any username";

            // Act
            var actualReturnValue = logFileParserHubService.GetParsingSessionId(username);

            // Assert
            Assert.That(actualReturnValue, Is.EqualTo(expectedReturnValue));
        }
    }
}
