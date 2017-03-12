using System;
using System.Collections.Concurrent;

using Moq;
using NUnit.Framework;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;
using Parser.Common.Interceptors.Tests.Mocks;

namespace Parser.Common.Interceptors.Tests.HttpContextCachingInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIInterceptorInstance_WhenParametersAreValid()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act
            var actualInstance = new HttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IInterceptor>());
        }

        [Test]
        public void IntializeLastCacheUpdateTimestampsByMethodNameField_AsConcurrentDictionary()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act
            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);
            var actualLastCacheUpdateTimestampsByMethodName = httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName;

            // Assert
            Assert.That(actualLastCacheUpdateTimestampsByMethodName, Is.Not.Null);
            Assert.That(actualLastCacheUpdateTimestampsByMethodName, Is.InstanceOf<ConcurrentDictionary<string, DateTime>>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIHttpContextCacheProviderParameterIsNull()
        {
            // Arrange
            IHttpContextCacheProvider httpContextCacheProvider = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act & Assert
            Assert.That(
                () => new HttpContextCachingInterceptor(httpContextCacheProvider, dateTimeProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IHttpContextCacheProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIDateTimeProviderParameterIsNull()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            IDateTimeProvider dateTimeProvider = null;

            // Act & Assert
            Assert.That(
                () => new HttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IDateTimeProvider)));
        }
    }
}
