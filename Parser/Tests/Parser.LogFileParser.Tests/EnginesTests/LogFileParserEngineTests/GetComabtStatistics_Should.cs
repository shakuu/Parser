using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.EnginesTests.LogFileParserEngineTests
{
    [TestFixture]
    public class GetComabtStatistics_Should
    {
        [Test]
        public void ReturnCorrectValue()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainerFactory = new Mock<ICombatStatisticsContainerFactory>();

            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var command = new Mock<ICommand>();

            var logFileParserEngine = new MockLogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainerFactory.Object);
            logFileParserEngine.CombatStatisticsContainer = combatStatisticsContainer.Object;

            var expectedResult = combatStatisticsContainer.Object;

            // Act
            var actualResult = logFileParserEngine.GetComabtStatistics();

            // Assert
            Assert.That(actualResult, Is.SameAs(expectedResult));
        }
    }
}
