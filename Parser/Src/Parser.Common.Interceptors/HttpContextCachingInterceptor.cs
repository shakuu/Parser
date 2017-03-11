using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class HttpContextCachingInterceptor : IInterceptor
    {
        private const double CacheTimeoutPeriodInMinutes = 5;

        private readonly IHttpContextCacheProvider httpContextCacheProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        private readonly IDictionary<string, DateTime> lastCacheUpdateTimestampsByMethodName;

        public HttpContextCachingInterceptor(IHttpContextCacheProvider httpContextCacheProvider, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(httpContextCacheProvider, nameof(IHttpContextCacheProvider)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.httpContextCacheProvider = httpContextCacheProvider;
            this.dateTimeProvider = dateTimeProvider;

            this.lastCacheUpdateTimestampsByMethodName = new ConcurrentDictionary<string, DateTime>();
        }

        public void Intercept(IInvocation invocation)
        {
            var invokedMethodName = this.GetInvokedMethodName(invocation);

            var invokedMethodTimeElapsedSincePreviousCacheUpdateInMinutes = this.GetTimeElapsedSincePreviousCacheUpdateInMinutes(invokedMethodName);
            if (invokedMethodTimeElapsedSincePreviousCacheUpdateInMinutes < HttpContextCachingInterceptor.CacheTimeoutPeriodInMinutes)
            {
                invocation.ReturnValue = this.httpContextCacheProvider[invokedMethodName];
            }
            else
            {
                invocation.Proceed();
                this.UpdateCacheForMethod(invokedMethodName, invocation.ReturnValue);
            }
        }

        private void UpdateCacheForMethod(string methodName, object data)
        {
            if (!this.lastCacheUpdateTimestampsByMethodName.ContainsKey(methodName))
            {
                this.lastCacheUpdateTimestampsByMethodName.Add(methodName, this.dateTimeProvider.GetUtcNow());
            }

            this.lastCacheUpdateTimestampsByMethodName[methodName] = this.dateTimeProvider.GetUtcNow();
            this.httpContextCacheProvider[methodName] = data;
        }

        private string GetInvokedMethodName(IInvocation invocation)
        {
            var invokedMethodName = invocation.Request.Method.Name;

            return invokedMethodName;
        }

        private double GetTimeElapsedSincePreviousCacheUpdateInMinutes(string invokedMethodName)
        {
            double timeElapsed;

            var invokedMethodDataIsCached = this.lastCacheUpdateTimestampsByMethodName.ContainsKey(invokedMethodName);
            if (invokedMethodDataIsCached)
            {
                var lastCacheUpdateTimestamp = this.lastCacheUpdateTimestampsByMethodName[invokedMethodName];
                timeElapsed = (this.dateTimeProvider.GetUtcNow() - lastCacheUpdateTimestamp).TotalMinutes;
            }
            else
            {
                timeElapsed = double.MaxValue;
            }

            return timeElapsed;
        }
    }
}
