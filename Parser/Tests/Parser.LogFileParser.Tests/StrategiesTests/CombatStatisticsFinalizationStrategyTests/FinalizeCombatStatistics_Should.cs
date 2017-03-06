using System;
using System.Collections;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.Common.Models;
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

            var combatStatisticsFinalizationStrategy = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

            ICombatStatistics combatStatistics = null;

            // Act & Assert
            Assert.That(
                () => combatStatisticsFinalizationStrategy.FinalizeCombatStatistics(combatStatistics),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatistics)));
        }

        [TestCaseSource(typeof(FinalizeCombatStatistics_Should), "TestCases")]
        public void ReturnIFinalizedCombatStatisticsInstance_WithCorrectData(string characterName, double damageDone, double damageTaken, string enterCombatTime, string exitCombatTime)
        {
            // Arrange
            var finalizedCombatStatisticsFactory = new Mock<IFinalizedCombatStatisticsFactory>();
            var finalizedCombatStatistics = new FinalizedCombatStatistics();
            finalizedCombatStatisticsFactory.Setup(f => f.CreateFinalizedCombatStatistics()).Returns(finalizedCombatStatistics);

            var combatStatisticsFinalizationStrategy = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatistics.SetupGet(s => s.CharacterName).Returns(characterName);
            combatStatistics.SetupGet(s => s.DamageDone).Returns(damageDone);
            combatStatistics.SetupGet(s => s.DamageTaken).Returns(damageTaken);
            combatStatistics.SetupGet(s => s.EnterCombatTime).Returns(DateTime.Parse(enterCombatTime));
            combatStatistics.SetupGet(s => s.ExitCombatTime).Returns(DateTime.Parse(exitCombatTime));

            var actualFinalizedCommand = combatStatisticsFinalizationStrategy.FinalizeCombatStatistics(combatStatistics.Object);

            // Assert
            Assert.That(actualFinalizedCommand.CharacterName, Is.EqualTo(characterName));
            Assert.That(actualFinalizedCommand.DamageDone, Is.EqualTo(damageDone));
            Assert.That(actualFinalizedCommand.DamageTaken, Is.EqualTo(damageTaken));
            Assert.That(actualFinalizedCommand.CombatDurationInSeconds, Is.EqualTo(60));
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("name", 1200, 1200, "22:38:00.000", "22:39:00.000");
            }
        }
    }
}
