using Bytes2you.Validation;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class CachingInterceptor : IInterceptor
    {
        private const double CacheTimeoutPeriodInMinutes = 1;

        private readonly ICacheProvider cacheProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public CachingInterceptor(ICacheProvider cacheProvider, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(cacheProvider, nameof(ICacheProvider)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.cacheProvider = cacheProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            var invokedMethodName = this.GetInvokedMethodName(invocation);

            var cachedReturnValueForMethod = this.cacheProvider[invokedMethodName];
            if (cachedReturnValueForMethod != null)
            {
                invocation.ReturnValue = cachedReturnValueForMethod;
            }
            else
            {
                invocation.Proceed();

                this.CacheReturnValueForMethod(invokedMethodName, invocation.ReturnValue);
            }
        }

        private void CacheReturnValueForMethod(string methodName, object returnValue)
        {
            var absoluteExpiration = this.dateTimeProvider.GetUtcNow().AddMinutes(CachingInterceptor.CacheTimeoutPeriodInMinutes);

            this.cacheProvider.Add(methodName, returnValue, absoluteExpiration);
        }

        protected virtual string GetInvokedMethodName(IInvocation invocation)
        {
            var invokedMethodName = invocation.Request.Method.Name;

            return invokedMethodName;
        }
    }
}
