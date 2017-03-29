using System;

using Moq;
using NUnit.Framework;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors.Tests.CachingInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIInterceptorInstance_WhenParametersAreValid()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act
            var actualInstance = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IInterceptor>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICacheProviderParameterIsNull()
        {
            // Arrange
            ICacheProvider cacheProvider = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act & Assert
            Assert.That(
                () => new CachingInterceptor(cacheProvider, dateTimeProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICacheProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIDateTimeProviderIsNull()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            IDateTimeProvider dateTimeProvider = null;

            // Act & Assert
            Assert.That(
                () => new CachingInterceptor(cacheProvider.Object, dateTimeProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IDateTimeProvider)));
        }
    }
}
