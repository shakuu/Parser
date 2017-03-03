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
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

            // Act
            logFileParserHubService.GetParsingSessionId();

            // Assert
            logFileParserEngineManager.Verify(m => m.StartNewLogFileParserEngine(), Times.Once);
        }

        [Test]
        public void ShouldReturnCorrectStringValue()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

            var expectedReturnValue = "expected return value";
            logFileParserEngineManager.Setup(m => m.StartNewLogFileParserEngine()).Returns(expectedReturnValue);

            // Act
            var actualReturnValue = logFileParserHubService.GetParsingSessionId();

            // Assert
            Assert.That(actualReturnValue, Is.EqualTo(expectedReturnValue));
        }
    }
}
