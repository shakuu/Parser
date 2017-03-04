using Moq;
using NUnit.Framework;
using Parser.Common.Contracts;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.ExitCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class HandleCommand_Should
    {
        [Test]
        public void InvokeICommandParameter_TimeStampPropertyGetMethodOnce()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            //var combatStatistics 
        }
    }
}
