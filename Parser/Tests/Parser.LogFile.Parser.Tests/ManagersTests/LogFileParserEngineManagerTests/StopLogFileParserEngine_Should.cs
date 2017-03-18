using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Factories;
using Parser.LogFileParser.Managers;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.ManagersTests.LogFileParserEngineManagerTests
{
    [TestFixture]
    public class StopLogFileParserEngine_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            string engineId = null;

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.StopLogFileParserEngine(engineId),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void ThrowArgumenException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = string.Empty;

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.StopLogFileParserEngine(engineId),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void ThrowArgumentException_WhenLogFileParserEnginesDictionaryDoesNotContainTheRequestedKey()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = "any engine id";

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.StopLogFileParserEngine(engineId),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains("Requested engine not found."));
        }

        [Test]
        public void RemoveILogFileParserEngineWithCorrectEngineId_FromLogFileParserEnginesDictionary()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = "any engine id";
            var logFileParserEngine = new Mock<ILogFileParserEngine>();

            var logFileParserEngineManager = new MockLogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);
            logFileParserEngineManager.LogFileParserEngines.Add(engineId, logFileParserEngine.Object);

            // Act
            logFileParserEngineManager.StopLogFileParserEngine(engineId);
            var logFileParserEnginesContainsEngineIdKey = logFileParserEngineManager.LogFileParserEngines.ContainsKey(engineId);

            // Assert
            Assert.That(logFileParserEnginesContainsEngineIdKey, Is.False);
        }
    }
}
