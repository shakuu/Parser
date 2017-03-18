using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Strategies;

namespace Parser.LogFileParser.Tests.StrategiesTests.CombatStatisticsFinalizationStrategyTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICombatStatisticsFinalizationStrategyInstance_WhenParametersAreValid()
        {
            // Arrange
            var finalizedCombatStatisticsFactory = new Mock<IFinalizedCombatStatisticsFactory>();

            // Act
            var actualInstance = new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICombatStatisticsFinalizationStrategy>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIFinalizedCombatStatisticsFactoryIsNull()
        {
            // Arrange
            IFinalizedCombatStatisticsFactory finalizedCombatStatisticsFactory = null;

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsFinalizationStrategy(finalizedCombatStatisticsFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IFinalizedCombatStatisticsFactory)));
        }
    }
}
