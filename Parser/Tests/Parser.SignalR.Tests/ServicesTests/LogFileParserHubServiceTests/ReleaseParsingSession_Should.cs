using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.SignalR.Services;

namespace Parser.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
{
    [TestFixture]
    public class ReleaseParsingSession_Should
    {
        [Test]
        public void ShouldInvokeILogFileParserEngineManager_ReleaseParsingSessionMethodOnceWithCorrectParameter()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var commandJsonConvertProvider = new Mock<ICommandJsonConvertProvider>();

            var logFileParserHubService = new LogFileParserHubService(logFileParserEngineManager.Object, commandJsonConvertProvider.Object);

            var engineId = "any engine id";

            // Act
            logFileParserHubService.ReleaseParsingSession(engineId);

            // Assert
            logFileParserEngineManager.Verify(m => m.StopLogFileParserEngine(engineId), Times.Once);
        }
    }
}
