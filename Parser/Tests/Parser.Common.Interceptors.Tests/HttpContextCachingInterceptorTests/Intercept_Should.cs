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
        public void InvokeIHttpContextCacheProvider_IndexerGetMethod_WhenTimeElapsedIsLessThanCacheTimeout()
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
        public void InvokeIInvocation_ReturnValueSetMethodOnceWithCorrectParameter_WhenTimeElapsedIsLessThanCacheTimeout()
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

        [Test]
        public void InvokeIInvocation_ProceedMethod_WhenTimeElapsedIsMoreThanCacheTimeout()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(p => p.GetUtcNow()).Returns(DateTime.Parse("00:05:30.000"));

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";
            httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName.Add(methodName, DateTime.Parse("00:00:00.000"));

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.Verify(i => i.Proceed(), Times.Once);
        }

        [Test]
        public void CreateEntryWithCorrectKeyInLastCacheUpdateTimestampsByMethodName_WhenTimeElapsedIsMoreThanCacheTimeout()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            dateTimeProvider.SetupSequence(p => p.GetUtcNow()).Returns(DateTime.Parse("00:05:30.000"));

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);
            invocation.SetupGet(i => i.ReturnValue).Returns(new Object());

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);
            var methodNameKeyExists = httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName.ContainsKey(methodName);

            // Assert
            Assert.That(methodNameKeyExists, Is.True);
        }

        [Test]
        public void UpdateEntryWithCorrectKeyInLastCacheUpdateTimestampsByMethodName_WhenTimeElapsedIsMoreThanCacheTimeout()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var expectedTimestamp = DateTime.Parse("00:06:30.000");
            dateTimeProvider.SetupSequence(p => p.GetUtcNow()).Returns(DateTime.Parse("00:05:30.000")).Returns(expectedTimestamp);

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);
            invocation.SetupGet(i => i.ReturnValue).Returns(new Object());

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);
            var actualTimestamp = httpContextCachingInterceptor.LastCacheUpdateTimestampsByMethodName[methodName];

            // Assert
            Assert.That(actualTimestamp, Is.EqualTo(expectedTimestamp));
        }

        [Test]
        public void InvokeIHttpContextCacheProvider_IndexerSetMethodOnceWithCorrectParameter_WhenTimeElapsedIsMoreThanCacheTimeout()
        {
            // Arrange
            var httpContextCacheProvider = new Mock<IHttpContextCacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            dateTimeProvider.SetupSequence(p => p.GetUtcNow()).Returns(DateTime.Parse("00:05:30.000"));

            var httpContextCachingInterceptor = new MockHttpContextCachingInterceptor(httpContextCacheProvider.Object, dateTimeProvider.Object);

            var methodName = "any string";

            var invocation = new Mock<IInvocation>();
            invocation.Setup(i => i.Request.Method.Name).Returns(methodName);

            var expectedData = new Object();
            invocation.SetupGet(i => i.ReturnValue).Returns(expectedData);

            // Act
            httpContextCachingInterceptor.Intercept(invocation.Object);

            // Assert
            httpContextCacheProvider.VerifySet(p => p[methodName] = expectedData, Times.Once);
        }
    }
}
