using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.SignalR;

using Bytes2you.Validation;

using Ninject;

namespace Parser.MvcClient.App_Start.SignalRHubDependencyResolver
{
    public class ParserHubConfigurationDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public ParserHubConfigurationDependencyResolver(IKernel kernel)
        {
            Guard.WhenArgument(kernel, nameof(IKernel)).IsNull().Throw();

            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>() { this.kernel.Get(serviceType) };
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            this.kernel.Bind(serviceType).ToMethod(ctx => activators.FirstOrDefault().Invoke());
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            this.kernel.Bind(serviceType).ToMethod(ctx => activator.Invoke());
        }

        public void Dispose()
        {
        }
    }
}