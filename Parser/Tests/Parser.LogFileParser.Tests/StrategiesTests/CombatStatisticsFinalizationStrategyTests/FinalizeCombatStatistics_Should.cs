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
        public void ReturnIFinalizedCombatStatisticsInstance_WithCorrectData(string expectedCharacterName, double expectedDamageDone, double expectedDamageTaken, string enterCombatTime, string exitCombatTime, double expectedDamageDonePerSecond, double expectedDamageTakenPerSecond, double expectedCombatDurationInSeconds)
        {
            // Arrange
            var finalizedCombatStatisticsFactory = new Mock<IFinalizedCombatStatisticsFactory>();
            var finalizedCombatStatistics = new FinalizedCombatStatistics();
            finalizedCombatStatisticsFactory.Setup(f => f.CreateFinalizedCombatStatistics()).Returns(finalizedCombatStatistics);

            var combatStatisticsFinalizationStrategy = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatistics.SetupGet(s => s.CharacterName).Returns(expectedCharacterName);
            combatStatistics.SetupGet(s => s.DamageDone).Returns(expectedDamageDone);
            combatStatistics.SetupGet(s => s.DamageTaken).Returns(expectedDamageTaken);
            combatStatistics.SetupGet(s => s.EnterCombatTime).Returns(DateTime.Parse(enterCombatTime));
            combatStatistics.SetupGet(s => s.ExitCombatTime).Returns(DateTime.Parse(exitCombatTime));

            var actualFinalizedCommand = combatStatisticsFinalizationStrategy.FinalizeCombatStatistics(combatStatistics.Object);

            // Assert
            Assert.That(actualFinalizedCommand.CharacterName, Is.EqualTo(expectedCharacterName));
            Assert.That(actualFinalizedCommand.DamageDone, Is.EqualTo(expectedDamageDone));
            Assert.That(actualFinalizedCommand.DamageTaken, Is.EqualTo(expectedDamageTaken));
            Assert.That(actualFinalizedCommand.DamageDonePerSecond, Is.EqualTo(expectedDamageDonePerSecond));
            Assert.That(actualFinalizedCommand.DamageTakenPerSecond, Is.EqualTo(expectedDamageTakenPerSecond));
            Assert.That(actualFinalizedCommand.CombatDurationInSeconds, Is.EqualTo(expectedCombatDurationInSeconds));
        }

        public static IEnumerable TestCases
        {
            get
            {
                // Expected parameters: 
                // string expectedCharacterName, double expectedDamageDone, double expectedDamageTaken, string enterCombatTime, string exitCombatTime, double expectedDamageDonePerSecond, double expectedDamageTakenPerSecond, double expectedCombatDurationInSeconds
                yield return new TestCaseData("name", 1200, 1200, "22:38:00.000", "22:39:00.000", 20, 20, 60);
                yield return new TestCaseData("name", 1200, 1200, "22:38:00.000", "22:40:00.000", 10, 10, 120);
                yield return new TestCaseData("another name", 1200, 1200, "22:38:00.000", "22:40:00.000", 10, 10, 120);
                yield return new TestCaseData("another name", 2400, 2400, "22:38:00.000", "22:40:00.000", 20, 20, 120);
                yield return new TestCaseData("another name", 2400, 2400, "22:38:00.000", "22:38:30.000", 80, 80, 30);
            }
        }
    }
}
