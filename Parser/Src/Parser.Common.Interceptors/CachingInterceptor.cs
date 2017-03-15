using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class CachingInterceptor : IInterceptor
    {
        private const double CacheTimeoutPeriodInMinutes = 5;

        private readonly ICacheProvider cacheProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public CachingInterceptor(ICacheProvider cacheProvider, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(cacheProvider, nameof(IHttpContextCacheProvider)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.cacheProvider = cacheProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            var invokedMethodName = this.GetInvokedMethodName(invocation);

            if (this.cacheProvider[invokedMethodName] != null)
            {
                invocation.ReturnValue = cacheProvider[invokedMethodName];
            }
            else
            {
                invocation.Proceed();

                this.cacheProvider.Add(invokedMethodName, invocation.ReturnValue, this.dateTimeProvider.GetUtcNow().AddMinutes(CachingInterceptor.CacheTimeoutPeriodInMinutes));
            }
        }

        private string GetInvokedMethodName(IInvocation invocation)
        {
            var invokedMethodName = invocation.Request.Method.Name;

            return invokedMethodName;
        }
    }
}
