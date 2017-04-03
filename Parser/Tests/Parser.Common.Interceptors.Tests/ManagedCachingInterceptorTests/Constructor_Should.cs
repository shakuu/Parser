using System;

using Ninject.Extensions.Interception;

using Moq;
using NUnit.Framework;

namespace Parser.Common.Interceptors.Tests.ManagedCachingInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectInstance_WhenParametersAreValid()
        {
            // Arrange 
            var shouldCacheInvocationReturnValueStrategy = new Mock<IShouldCacheInvocationReturnValueStrategy>();
            var decoratedCachingInterceptor = new Mock<ICachingInterceptor>();

            // Act
            var actualInstance = new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy.Object, decoratedCachingInterceptor.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IInterceptor>());
            Assert.That(actualInstance, Is.InstanceOf<ICachingInterceptor>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIShouldCacheInvocationReturnValueStrategyParameterIsNull()
        {
            // Arrange 
            IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy = null;
            var decoratedCachingInterceptor = new Mock<ICachingInterceptor>();

            // Act & Assert
            Assert.That(
                () => new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy, decoratedCachingInterceptor.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IShouldCacheInvocationReturnValueStrategy)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICachingInterceptorParameterIsNull()
        {
            // Arrange 
            var shouldCacheInvocationReturnValueStrategy = new Mock<IShouldCacheInvocationReturnValueStrategy>();
            ICachingInterceptor decoratedCachingInterceptor = null;

            // Act & Assert
            Assert.That(
                () => new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy.Object, decoratedCachingInterceptor),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICachingInterceptor)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange 
            IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy = null;
            ICachingInterceptor decoratedCachingInterceptor = null;

            // Act & Assert
            Assert.That(
                () => new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy, decoratedCachingInterceptor),
                Throws.InstanceOf<ArgumentNullException>());
        }
    }
}
