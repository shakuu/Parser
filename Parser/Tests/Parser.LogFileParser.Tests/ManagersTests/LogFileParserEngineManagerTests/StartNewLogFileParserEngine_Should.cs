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
    public class StartNewLogFileParserEngine_Should
    {
        [Test]
        public void InvokeIGuidStringProvider_NewGuidMethodOnce()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var guidString = "any string";
            guidStringProvider.Setup(p => p.NewGuidString()).Returns(guidString);

            var logFileParserEngine = new Mock<ILogFileParserEngine>();
            logFileParserEngineFactory.Setup(f => f.CreateLogFileParserEngine()).Returns(logFileParserEngine.Object);

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act
            logFileParserEngineManager.StartLogFileParserEngine();

            // Assert
            guidStringProvider.Verify(p => p.NewGuidString(), Times.Once);
        }

        [Test]
        public void InvokeILogFileParserEngineFactory_CreateLogFileParserEngineMethodOnce()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var guidString = "any string";
            guidStringProvider.Setup(p => p.NewGuidString()).Returns(guidString);

            var logFileParserEngine = new Mock<ILogFileParserEngine>();
            logFileParserEngineFactory.Setup(f => f.CreateLogFileParserEngine()).Returns(logFileParserEngine.Object);

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act
            logFileParserEngineManager.StartLogFileParserEngine();

            // Assert
            logFileParserEngineFactory.Verify(f => f.CreateLogFileParserEngine(), Times.Once);
        }

        [Test]
        public void AddCorrectKeyAndEngine_ToLogFileParserEnginesDictionary()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var guidString = "any string";
            guidStringProvider.Setup(p => p.NewGuidString()).Returns(guidString);

            var logFileParserEngine = new Mock<ILogFileParserEngine>();
            logFileParserEngineFactory.Setup(f => f.CreateLogFileParserEngine()).Returns(logFileParserEngine.Object);

            var logFileParserEngineManager = new MockLogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            var expectedAddedEngine = logFileParserEngine.Object;

            // Act
            logFileParserEngineManager.StartLogFileParserEngine();

            var logFileParserEnginesDictionaryContainsKey = logFileParserEngineManager.LogFileParserEngines.ContainsKey(guidString);
            var actuallogFileParserEnginesDictionaryEngine = logFileParserEngineManager.LogFileParserEngines[guidString];

            // Assert
            Assert.That(logFileParserEnginesDictionaryContainsKey, Is.True);
            Assert.That(actuallogFileParserEnginesDictionaryEngine, Is.SameAs(expectedAddedEngine));
        }

        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            var guidString = "any string";
            guidStringProvider.Setup(p => p.NewGuidString()).Returns(guidString);

            var logFileParserEngine = new Mock<ILogFileParserEngine>();
            logFileParserEngineFactory.Setup(f => f.CreateLogFileParserEngine()).Returns(logFileParserEngine.Object);

            var logFileParserEngineManager = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Act 
            var actualResult = logFileParserEngineManager.StartLogFileParserEngine();

            // Assert
            Assert.That(actualResult, Is.EqualTo(guidString));
        }
    }
}
