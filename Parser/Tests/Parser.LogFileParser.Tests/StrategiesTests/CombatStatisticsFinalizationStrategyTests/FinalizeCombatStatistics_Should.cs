using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Strategies;

namespace Parser.LogFileParser.Tests.StrategiesTests.CombatStatisticsFinalizationStrategyTests
{
    [TestFixture]
    public class FinalizeCombatStatistics_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsParameterIsNull()
        {
            // Arrange
            var finalizedCombatStatisticsFactory = new Mock<IFinalizedCombatStatisticsFactory>();

            var actualInstance = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

            ICombatStatistics combatStatistics = null;

            // Act & Assert
            Assert.That(
                () => actualInstance.FinalizeCombatStatistics(combatStatistics),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatistics)));
        }

        [Test]
        public void IFinalizedCombatStatisticsInstance_WithCorrectData()
        {
            // Arrange
            var finalizedCombatStatisticsFactory = new Mock<IFinalizedCombatStatisticsFactory>();

            var actualInstance = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

        }
    }
}
