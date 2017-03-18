using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Factories;
using Parser.LogFile.Parser.Managers;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.ManagersTests.LogFileParserEngineManagerTests
{
    [TestFixture]
    public class EnqueueCommandToEngineWithId_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            string engineId = null;
            var command = new Mock<ICommand>();

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void ThrowArgumenException_WhenEngineIdParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = string.Empty;
            var command = new Mock<ICommand>();

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command.Object),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(engineId)));
        }

        [Test]
        public void ThrowArgumenNullException_WhenICommandParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = "any engine id";
            ICommand command = null;

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommand)));
        }

        [Test]
        public void ThrowArgumentException_WhenLogFileParserEnginesDictionaryDoesNotContainTheRequestedKey()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = "any engine id";
            var command = new Mock<ICommand>();

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act & Assert
            Assert.That(
                () => logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command.Object),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains("Requested engine not found."));
        }

        [Test]
        public void InvokeEnqueueCommandMethod_OfTheCorrectILogFileParserEngineInstance()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var engineId = "any engine id";
            var command = new Mock<ICommand>();

            var logFileParserEngine = new Mock<ILogFileParserEngine>();

            var logFileParserEngineManager = new MockLogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);
            logFileParserEngineManager.LogFileParserEngines.Add(engineId, logFileParserEngine.Object);

            // Act 
            logFileParserEngineManager.EnqueueCommandToEngineWithId(engineId, command.Object);

            // Assert
            logFileParserEngine.Verify(e => e.EnqueueCommand(command.Object), Times.Once);
        }
    }
}
