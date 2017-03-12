using System;

using Moq;
using NUnit.Framework;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Interceptors.Tests.Mocks;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors.Tests.HttpContextCachingInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void InvokeIHttpContextCacheProvider_IndexerGetMethodWhenTimeElapsedIsLessThanCacheTimeout()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(p => p.GetUtcNow()).Returns(DateTime.Parse("00:00:30.000"));

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";
            httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName.Add(methodName, DateTime.Parse("00:00:00.000"));

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);

            // Assert
            httpContextCacheProvider.Verify(p => p[methodName], Times.Once);
        }

        [Test]
        public void InvokeIInvocation_ReturnValueSetMethodOnceWithCorrectParameter()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(p => p.GetUtcNow()).Returns(DateTime.Parse("00:00:30.000"));

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";
            httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName.Add(methodName, DateTime.Parse("00:00:00.000"));

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);

            var expectedSetParameter = new Object();
            httpContextCacheProvider.Setup(p => p[It.IsAny<string>()]).Returns(expectedSetParameter);

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.VerifySet(i => i.ReturnValue = expectedSetParameter, Times.Once);
        }
    }
}
