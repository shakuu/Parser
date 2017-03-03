using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Factories;
using Parser.LogFileParser.Managers;

namespace Parser.LogFileParser.Tests.ManagersTests.LogFileParserEngineManagerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectILogFileParserEngineManagerInstance_WhenParametersAreValid()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            // Act
            var actualInstance = new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ILogFileParserEngineManager>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIGuidStringProviderParameterIsNull()
        {
            // Arrange
            IGuidStringProvider guidStringProvider = null;
            var logFileParserEngineFactory = new Mock<ILogFileParserEngineFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngineManager(guidStringProvider, logFileParserEngineFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IGuidStringProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenILogFileParserEngineFactoryParameterIsNull()
        {
            // Arrange
            var guidStringProvider = new Mock<IGuidStringProvider>();
            ILogFileParserEngineFactory logFileParserEngineFactory = null;

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngineManager(guidStringProvider.Object, logFileParserEngineFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILogFileParserEngineFactory)));
        }
    }
}
