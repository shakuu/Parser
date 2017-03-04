using NUnit.Framework;

using Parser.LogFileParser.CommandResolutionHandlers;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.ExitCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICommandResolutionHandlerInstance_WhenParametersAreValid()
        {
            // Act
            var actualInstance = new ExitCombatCommandResolutionHandler();

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICommandResolutionHandler>());
            Assert.That(actualInstance, Is.InstanceOf<ICommandResolutionHandlerChain>());
        }
    }
}
