using System;

using Moq;
using NUnit.Framework;

using Parser.LogFileParser.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Services;

namespace Parser.SignalR.Tests.ServicesTests.LogFileParserHubServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectILogFileParserHubServiceInstance_WhenParametersAreValid()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            // Act
            var actualInstance = new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ILogFileParserHubService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenILogFileParserEngineManagerParameterIsNull()
        {
            // Arrange
            ILogFileParserEngineManager logFileParserEngineManager = null;
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserHubService(logFileParserEngineManager, jsonConvertProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ILogFileParserEngineManager)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIJsonConvertProviderParameterIsNull()
        {
            // Arrange
            var logFileParserEngineManager = new Mock<ILogFileParserEngineManager>();
            IJsonConvertProvider jsonConvertProvider = null;

            // Act & Assert
            Assert.That(
                () => new LogFileParserHubService(logFileParserEngineManager.Object, jsonConvertProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IJsonConvertProvider)));
        }
    }
}
