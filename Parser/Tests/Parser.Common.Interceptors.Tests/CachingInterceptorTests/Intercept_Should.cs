using System;

using Moq;
using NUnit.Framework;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors.Tests.CachingInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void InvokeIInvocation_RequestMethodNamePropertyGetMethod()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var cachingInterceptor = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            var invokedMethodName = "any string";
            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.Request.Method.Name).Returns(invokedMethodName);

            // Act
            cachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.VerifyGet(i => i.Request.Method.Name, Times.Once);
        }

        [Test]
        public void InvokeICacheProvider_IndexerGetMethodOnceWithCorrectParameter()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var cachingInterceptor = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            var invokedMethodName = "any string";
            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.Request.Method.Name).Returns(invokedMethodName);

            // Act
            cachingInterceptor.Intercept(invocation.Object);

            // Assert
            cacheProvider.Verify(p => p[invokedMethodName], Times.Once);
        }

        [Test]
        public void AssignCorrectValueToIInvocationReturnValueProperty_WhenCachedDataIsAvailable()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var cachingInterceptor = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            var invokedMethodName = "any string";
            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.Request.Method.Name).Returns(invokedMethodName);

            var cachedData = new Object();
            cacheProvider.SetupGet(p => p[It.IsAny<string>()]).Returns(cachedData);

            // Act
            cachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.VerifySet(i => i.ReturnValue = cachedData, Times.Once);
        }

        [Test]
        public void InvokeIInvocation_ProceedMethod_WhenCachedDataIsNotAvailable()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var cachingInterceptor = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            var invokedMethodName = "any string";
            var invocation = new Mock<IInvocation>();
            invocation.SetupGet(i => i.Request.Method.Name).Returns(invokedMethodName);

            // Act
            cachingInterceptor.Intercept(invocation.Object);

            // Assert
            invocation.Verify(i => i.Proceed(), Times.Once);
        }

        [Test]
        public void InvokeICacheProvider_AddMethodOnceWithCorrectParameters()
        {
            // Arrange
            var cacheProvider = new Mock<ICacheProvider>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            var cachingInterceptor = new CachingInterceptor(cacheProvider.Object, dateTimeProvider.Object);

            var invocation = new Mock<IInvocation>();

            var invokedMethodName = "any string";
            invocation.SetupGet(i => i.Request.Method.Name).Returns(invokedMethodName);

            var methodReturnValue = new Object();
            invocation.SetupGet(p => p.ReturnValue).Returns(methodReturnValue);

            var dateTimeProviderUtcNow = DateTime.Parse("00:00:00.000");
            dateTimeProvider.Setup(p => p.GetUtcNow()).Returns(dateTimeProviderUtcNow);

            var expectedAbsoluteExpirationParameterValue = dateTimeProviderUtcNow.AddMinutes(5);

            // Act
            cachingInterceptor.Intercept(invocation.Object);

            // Assert
            cacheProvider.Verify(p => p.Add(invokedMethodName, methodReturnValue, expectedAbsoluteExpirationParameterValue), Times.Once);
        }
    }
}
