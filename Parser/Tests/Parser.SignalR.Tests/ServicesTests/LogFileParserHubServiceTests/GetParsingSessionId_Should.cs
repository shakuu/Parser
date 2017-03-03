using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Services;

namespace Parser.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
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

            // Act
            logFileParserHubService.GetParsingSessionId();

            // Assert
            logFileParserEngineManager.Verify(m => m.StartLogFileParserEngine(), Times.Once);
        }

        [Test]
        public void ShouldReturnCorrectStringValue()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var expectedReturnValue = "expected return value";
            logFileParserEngineManager.Setup(m => m.StartLogFileParserEngine()).Returns(expectedReturnValue);

            // Act
            var actualReturnValue = logFileParserHubService.GetParsingSessionId();

            // Assert
            Assert.That(actualReturnValue, Is.EqualTo(expectedReturnValue));
        }
    }
}
