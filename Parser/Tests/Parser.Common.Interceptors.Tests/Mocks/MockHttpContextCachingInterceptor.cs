using System;
using System.Collections.Generic;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors.Tests.Mocks
{
    internal class MockHttpContextCachingInterceptor : HttpContextCachingInterceptor
    {
        internal MockHttpContextCachingInterceptor(IHttpContextCacheProvider httpContextCacheProvider, IDateTimeProvider dateTimeProvider)
            : base(httpContextCacheProvider, dateTimeProvider)
        {
        }

        internal new IDictionary<string, DateTime> LastCacheUpdateTimestampsByMethodName { get { return base.LastCacheUpdateTimestampsByMethodName; } }
    }
}
