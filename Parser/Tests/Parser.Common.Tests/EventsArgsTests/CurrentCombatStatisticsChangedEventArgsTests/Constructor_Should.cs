using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;

namespace Parser.Common.Tests.EventsArgsTests.CurrentCombatStatisticsChangedEventArgsTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectEventArgsInstance_WhenParametersAreValid()
        {
            // Arrange
            var combatStatistics = new Mock<ICombatStatistics>();

            // Act
            var actualInstance = new CurrentCombatStatisticsChangedEventArgs(combatStatistics.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<EventArgs>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsParameterIsNull()
        {
            // Arrange
            ICombatStatistics combatStatistics = null;

            // Act & Assert
            Assert.That(
                () => new CurrentCombatStatisticsChangedEventArgs(combatStatistics),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatistics)));
        }
    }
}
