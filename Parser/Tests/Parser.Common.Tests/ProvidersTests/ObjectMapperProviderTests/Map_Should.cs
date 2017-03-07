using System;

using AutoMapper;

using Moq;
using NUnit.Framework;

using Parser.Common.Providers;

namespace Parser.Common.Tests.ProvidersTests.ObjectMapperProviderTests
{
    [TestFixture]
    public class Map_Should
    {
        [Test]
        public void InvokeIMapper_MapMethodOnceWithCorrectParameters()
        {
            // Arrange
            var mapper = new Mock<IMapper>();

            var objectMapperProvider = new ObjectMapperProvider(mapper.Object);

            var source = new Mock<object>();

            // Act
            objectMapperProvider.Map<object>(source.Object);

            // Assert
            mapper.Verify(m => m.Map<object>(source.Object), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenSourceParameterIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();

            var objectMapperProvider = new ObjectMapperProvider(mapper.Object);

            object source = null;

            // Act & Assert
            Assert.That(
                () => objectMapperProvider.Map<object>(source),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(source)));
        }
    }
}
