using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.EnterCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class HandleCommand_Should
    {
        [Test]
        public void InvokeICombatStatisticsFactory_CreateCombatStatisticsMethodOnce()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var allComabtStatistics = new Mock<ICollection<ICombatStatistics>>();
            combatStatisticsContainer.SetupGet(c => c.AllComabtStatistics).Returns(allComabtStatistics.Object);

            var currentComabtStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentComabtStatistics).Returns(currentComabtStatistics.Object);

            // Act 
            enterCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            combatStatisticsFactory.Verify(f => f.CreateCombatStatistics(), Times.Once);
        }

        [Test]
        public void AssignCorrectInstance_ToICombatStatisticsContainerParameterCurrentComabtStatisticsProperty()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            ICombatStatistics actualCombatStatisticsInstance = null;
            var currentCombatStatistics = new Mock<ICombatStatistics>();
            var allCombatStatistics = new Mock<ICollection<ICombatStatistics>>();
            combatStatisticsContainer.SetupGet(c => c.AllComabtStatistics).Returns(allCombatStatistics.Object);
            combatStatisticsContainer.SetupGet(c => c.CurrentComabtStatistics).Returns(currentCombatStatistics.Object);
            combatStatisticsContainer.SetupSet(c => c.CurrentComabtStatistics = It.IsAny<ICombatStatistics>()).Callback<ICombatStatistics>((combatStats) => actualCombatStatisticsInstance = combatStats);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsFactory.Setup(f => f.CreateCombatStatistics()).Returns(combatStatistics.Object);

            var expectedComabtStatisticsInstance = combatStatistics.Object;

            // Act 
            enterCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(actualCombatStatisticsInstance, Is.SameAs(expectedComabtStatisticsInstance));
        }

        [Test]
        public void InvokeICombatStatisticsContainerParameterAllComabtStatisticsProperty_AddMethodOnceWithCorrectParameter()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            var allCombatStatistics = new Mock<ICollection<ICombatStatistics>>();
            combatStatisticsContainer.SetupGet(c => c.AllComabtStatistics).Returns(allCombatStatistics.Object);
            combatStatisticsContainer.SetupGet(c => c.CurrentComabtStatistics).Returns(currentCombatStatistics.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsFactory.Setup(f => f.CreateCombatStatistics()).Returns(combatStatistics.Object);

            // Act 
            enterCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            allCombatStatistics.Verify(s => s.Add(currentCombatStatistics.Object), Times.Once);
        }

        [Test]
        public void InvokeICommandParameter_TimeStampPropertyGetMethodOnce()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            var allCombatStatistics = new Mock<ICollection<ICombatStatistics>>();
            combatStatisticsContainer.SetupGet(c => c.AllComabtStatistics).Returns(allCombatStatistics.Object);
            combatStatisticsContainer.SetupGet(c => c.CurrentComabtStatistics).Returns(currentCombatStatistics.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsFactory.Setup(f => f.CreateCombatStatistics()).Returns(combatStatistics.Object);

            // Act 
            enterCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            command.VerifyGet(c => c.TimeStamp, Times.Once);
        }

        [Test]
        public void ReturnTheSameICombatStatisticsContainerInstance()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            var allCombatStatistics = new Mock<ICollection<ICombatStatistics>>();
            combatStatisticsContainer.SetupGet(c => c.AllComabtStatistics).Returns(allCombatStatistics.Object);
            combatStatisticsContainer.SetupGet(c => c.CurrentComabtStatistics).Returns(currentCombatStatistics.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsFactory.Setup(f => f.CreateCombatStatistics()).Returns(combatStatistics.Object);

            var expectedReturnedInstance = combatStatisticsContainer.Object;

            // Act 
            var actualReturnedInstance = enterCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(actualReturnedInstance, Is.SameAs(expectedReturnedInstance));
        }
    }
}
