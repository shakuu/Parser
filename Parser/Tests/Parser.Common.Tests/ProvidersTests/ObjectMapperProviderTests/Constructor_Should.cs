using System;

using AutoMapper;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Providers;


namespace Parser.Common.Tests.ProvidersTests.ObjectMapperProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIObjectMapperProviderInstance_WhenParametersAreValid()
        {
            // Arrange
            var mapper = new Mock<IMapper>();

            // Act
            var actualInstance = new ObjectMapperProvider(mapper.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IObjectMapperProvider>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIMapperParameterIsNull()
        {
            // Arrange
            IMapper mapper = null;

            // Act & Assert
            Assert.That(
                () => new ObjectMapperProvider(mapper),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IMapper)));
        }
    }
}
