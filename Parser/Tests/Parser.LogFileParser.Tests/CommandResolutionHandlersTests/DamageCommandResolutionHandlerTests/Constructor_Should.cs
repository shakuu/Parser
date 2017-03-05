using NUnit.Framework;

using Parser.LogFileParser.CommandResolutionHandlers;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.DamageCommandResolutionHandlerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void PassCorrectMatchingEventNameValue()
        {
            // Arrange
            var expectedMatchingEventName = "Damage";

            // Act
            var damageCommandResolutionHandler = new MockDamageCommandResolutionHandler();
            
            // Assert
            Assert.That(damageCommandResolutionHandler.ExposedMatchingEventName, Is.EqualTo(expectedMatchingEventName));
        }

        [Test]
        public void CreateCorrectInstance()
        {
            // Arrange & Act
            var damageCommandResolutionHandler = new DamageCommandResolutionHandler();

            // Assert
            Assert.That(damageCommandResolutionHandler, Is.Not.Null);
            Assert.That(damageCommandResolutionHandler, Is.InstanceOf<ICommandResolutionHandler>());
            Assert.That(damageCommandResolutionHandler, Is.InstanceOf<ICommandResolutionHandlerChain>());
        }
    }
}
