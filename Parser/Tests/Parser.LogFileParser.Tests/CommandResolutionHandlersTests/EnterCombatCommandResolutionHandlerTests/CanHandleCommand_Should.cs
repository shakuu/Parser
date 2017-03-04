using Moq;
using NUnit.Framework;
using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.CommandResolutionHandlers;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.EnterCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class CanHandleCommand_Should
    {
        [TestCase("")]
        [TestCase(null)]
        public void ReturnFalse_WhenICommandObjectEventNamePropertyIsANullOrEmptyString(string eventName)
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new EnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            //var actualResult = enterCombatCommandResolutionHandler.
        }
    }
}
