using System;

using NUnit.Framework;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.CommandResolutionHandlersTests.BaseTests.CommandResolutionHandlerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenMatchingEventNameParameterIsNull()
        {
            // Arrange
            string matchingEventName = null;

            // Act & Assert
            Assert.That(
                () => new MockCommandResolutionHandler(matchingEventName),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(matchingEventName)));
        }

        [Test]
        public void ThrowArgumentException_WhenMatchingEventNameParameterIsEmpty()
        {
            // Arrange
            var matchingEventName = string.Empty;

            // Act & Assert
            Assert.That(
                () => new MockCommandResolutionHandler(matchingEventName),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(matchingEventName)));
        }
    }
}
