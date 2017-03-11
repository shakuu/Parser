using System;

using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class CachingInterceptor : IInterceptor
    {
        private readonly IHttpContextCacheProvider httpContextCacheProvider;

        public CachingInterceptor(IHttpContextCacheProvider httpContextCacheProvider)
        {
            Guard.WhenArgument(httpContextCacheProvider, nameof(IHttpContextCacheProvider)).IsNull().Throw();

            this.httpContextCacheProvider = httpContextCacheProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
