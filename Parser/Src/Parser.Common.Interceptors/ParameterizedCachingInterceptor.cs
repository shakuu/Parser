using System.Text;

using Ninject.Extensions.Interception;

using Parser.Common.Contracts;
using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Interceptors
{
    public class ParameterizedCachingInterceptor : CachingInterceptor, IInterceptor
    {
        public ParameterizedCachingInterceptor(ICacheProvider cacheProvider, IDateTimeProvider dateTimeProvider)
            : base(cacheProvider, dateTimeProvider)
        {
        }

        protected override string GetInvokedMethodName(IInvocation invocation)
        {
            var invokedMethodName = new StringBuilder();
            invokedMethodName.Append($"{invocation.Request.Target};{invocation.Request.Method.Name}");

            var invocationArguments = invocation.Request.Arguments;
            for (int invocationArgumentsIndex = 0; invocationArgumentsIndex < invocationArguments.Length; invocationArgumentsIndex++)
            {
                invokedMethodName.Append($";{invocationArgumentsIndex}={invocationArguments[invocationArgumentsIndex].ToString()}");
            }

            return invokedMethodName.ToString();
        }
    }
}
