using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class CachingInterceptor : IInterceptor
    {
        private const double CacheTimeoutPeriodInMinutes = 5;

        private readonly IHttpContextCacheProvider httpContextCacheProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        private readonly IDictionary<string, DateTime> lastCacheUpdateTimestamps;

        public CachingInterceptor(IHttpContextCacheProvider httpContextCacheProvider, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(httpContextCacheProvider, nameof(IHttpContextCacheProvider)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.httpContextCacheProvider = httpContextCacheProvider;
            this.dateTimeProvider = dateTimeProvider;

            this.lastCacheUpdateTimestamps = new ConcurrentDictionary<string, DateTime>();
        }

        public void Intercept(IInvocation invocation)
        {
            var invokedMethodName = this.GetInvokedMethodName(invocation);

            var invokedMethodTimeElapsedSincePreviousCacheUpdateInMinutes = this.GetTimeElapsedSincePreviousCacheUpdateInMinutes(invokedMethodName);
            if (invokedMethodTimeElapsedSincePreviousCacheUpdateInMinutes < CachingInterceptor.CacheTimeoutPeriodInMinutes)
            {
                invocation.ReturnValue = this.httpContextCacheProvider[invokedMethodName];
            }
            else
            {
                invocation.Proceed();

                this.httpContextCacheProvider[invokedMethodName] = invocation.ReturnValue;
                this.lastCacheUpdateTimestamps[invokedMethodName] = this.dateTimeProvider.GetUtcNow();
            }
        }

        private string GetInvokedMethodName(IInvocation invocation)
        {
            var invokedMethodName = invocation.Request.Method.Name;

            return invokedMethodName;
        }

        private double GetTimeElapsedSincePreviousCacheUpdateInMinutes(string invokedMethodName)
        {
            double timeElapsed;

            var invokedMethodDataIsCached = this.lastCacheUpdateTimestamps.ContainsKey(invokedMethodName);
            if (invokedMethodDataIsCached)
            {
                var lastCacheUpdateTimestamp = this.lastCacheUpdateTimestamps[invokedMethodName];
                timeElapsed = (this.dateTimeProvider.GetUtcNow() - lastCacheUpdateTimestamp).TotalMinutes;
            }
            else
            {
                this.lastCacheUpdateTimestamps.Add(invokedMethodName, this.dateTimeProvider.GetUtcNow());
                timeElapsed = double.MaxValue;
            }

            return timeElapsed;
        }
    }
}
