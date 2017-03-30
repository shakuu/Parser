using Ninject.Extensions.Interception;

namespace Parser.Common.Interceptors
{
    public interface IShouldCacheInvocationReturnValueStrategy
    {
        bool ShouldCacheReturnValue(IInvocation invocation);
    }
}
