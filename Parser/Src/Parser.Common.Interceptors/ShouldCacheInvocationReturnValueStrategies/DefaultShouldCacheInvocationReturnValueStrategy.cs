using Ninject.Extensions.Interception;

namespace Parser.Common.Interceptors.ShouldCacheInvocationReturnValueStrategies
{
    public class DefaultShouldCacheInvocationReturnValueStrategy : IShouldCacheInvocationReturnValueStrategy
    {
        public bool ShouldCacheReturnValue(IInvocation invocation)
        {
            var invokedMethodName = invocation.Request.Method.Name;

            return invokedMethodName != "ToString";
        }
    }
}
