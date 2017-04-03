using Ninject.Extensions.Interception;

using Moq;
using NUnit.Framework;

namespace Parser.Common.Interceptors.Tests.ManagedCachingInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void InvokeIShouldCacheInvocationReturnValueStrategyShouldCacheReturnValueMethodOnceWithCorrectParameter()
        {
            // Arrange 
            var shouldCacheInvocationReturnValueStrategy = new Mock<IShouldCacheInvocationReturnValueStrategy>();
            var decoratedCachingInterceptor = new Mock<ICachingInterceptor>();

            var managedCachingInterceptor = new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy.Object, decoratedCachingInterceptor.Object);

            var invocation = new Mock<IInvocation>();

            // Act
            managedCachingInterceptor.Intercept(invocation.Object);

            // Assert
            shouldCacheInvocationReturnValueStrategy.Verify(s => s.ShouldCacheReturnValue(invocation.Object), Times.Once);
        }

        [Test]
        public void InvokeICachingInterceptorInterceptMethodWithCorrectParameter_WhenIShouldCacheInvocationReturnValueStrategyReturnsTrue()
        {
            // Arrange 
            var shouldCacheInvocationReturnValueStrategy = new Mock<IShouldCacheInvocationReturnValueStrategy>();
            var decoratedCachingInterceptor = new Mock<ICachingInterceptor>();

            var managedCachingInterceptor = new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy.Object, decoratedCachingInterceptor.Object);

            shouldCacheInvocationReturnValueStrategy.Setup(s => s.ShouldCacheReturnValue(It.IsAny<IInvocation>())).Returns(true);

            var invocation = new Mock<IInvocation>();

            // Act
            managedCachingInterceptor.Intercept(invocation.Object);

            // Assert
            decoratedCachingInterceptor.Verify(i => i.Intercept(invocation.Object), Times.Once);
        }

        [Test]
        public void InvokeIInvocationProceedMethodWithCorrectParameter_WhenIShouldCacheInvocationReturnValueStrategyReturnsFalse()
        {
            // Arrange 
            var shouldCacheInvocationReturnValueStrategy = new Mock<IShouldCacheInvocationReturnValueStrategy>();
            var decoratedCachingInterceptor = new Mock<ICachingInterceptor>();

            var managedCachingInterceptor = new ManagedCachingInterceptor(shouldCacheInvocationReturnValueStrategy.Object, decoratedCachingInterceptor.Object);

            shouldCacheInvocationReturnValueStrategy.Setup(s => s.ShouldCacheReturnValue(It.IsAny<IInvocation>())).Returns(false);

            var invocation = new Mock<IInvocation>();

            // Act
            managedCachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.Verify(i => i.Proceed(), Times.Once);
        }
    }
}
