using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.EnginesTests.LogFileParserEngineTests
{
    [TestFixture]
    public class GetComabtStatistics_Should
    {
        [Test]
        public void ReturnCorrectValue()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedSubscribeProvider = new Mock<ICurrentCombatStatisticsChangedSubscribeProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedSubscribeProvider.Object);

            var command = new Mock<ICommand>();

            var logFileParserEngine = new MockLogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);
            logFileParserEngine.CombatStatisticsContainer = combatStatisticsContainer.Object;

            var expectedResult = combatStatisticsContainer.Object;

            // Act
            var actualResult = logFileParserEngine.GetCombatStatistics();

            // Assert
            Assert.That(actualResult, Is.SameAs(expectedResult));
        }
    }
}
