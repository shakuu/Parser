using Bytes2you.Validation;

using Ninject.Extensions.Interception;

namespace Parser.Common.Interceptors
{
    public class ManagedCachingInterceptor : IInterceptor
    {
        private readonly IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy;
        private readonly IInterceptor decoratedInterceptor;

        public ManagedCachingInterceptor(IShouldCacheInvocationReturnValueStrategy shouldCacheInvocationReturnValueStrategy, IInterceptor decoratedInterceptor)
        {
            Guard.WhenArgument(shouldCacheInvocationReturnValueStrategy, nameof(IShouldCacheInvocationReturnValueStrategy)).IsNull().Throw();
            Guard.WhenArgument(decoratedInterceptor, nameof(IInterceptor)).IsNull().Throw();

            this.shouldCacheInvocationReturnValueStrategy = shouldCacheInvocationReturnValueStrategy;
            this.decoratedInterceptor = decoratedInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            if (this.shouldCacheInvocationReturnValueStrategy.ShouldCacheReturnValue(invocation))
            {
                this.decoratedInterceptor.Intercept(invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
