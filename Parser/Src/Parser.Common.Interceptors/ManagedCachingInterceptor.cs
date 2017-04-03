using Bytes2you.Validation;

using Ninject.Extensions.Interception;

namespace Parser.Common.Interceptors
{
    public class ManagedCachingInterceptor : IInterceptor, ICachingInterceptor
    {
        private readonly IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy;
        private readonly ICachingInterceptor decoratedCachingInterceptor;

        public ManagedCachingInterceptor(IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy, ICachingInterceptor decoratedCachingInterceptor)
        {
            Guard.WhenArgument(shouldCacheInvocationReturnValueStrategy, nameof(IShouldCacheInvocationReturnValueStrategy)).IsNull().Throw();
            Guard.WhenArgument(decoratedCachingInterceptor, nameof(ICachingInterceptor)).IsNull().Throw();

            this.shouldCacheInvocationReturnValueStrategy = shouldCacheInvocationReturnValueStrategy;
            this.decoratedCachingInterceptor = decoratedCachingInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            if (this.shouldCacheInvocationReturnValueStrategy.ShouldCacheReturnValue(invocation))
            {
                this.decoratedCachingInterceptor.Intercept(invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
