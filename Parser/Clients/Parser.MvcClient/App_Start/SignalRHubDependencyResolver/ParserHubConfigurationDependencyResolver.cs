using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.SignalR;

using Bytes2you.Validation;

using Ninject;

namespace Parser.MvcClient.App_Start.SignalRHubDependencyResolver
{
    public class ParserHubConfigurationDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel kernel;

        public ParserHubConfigurationDependencyResolver(IKernel kernel)
        {
            Guard.WhenArgument(kernel, nameof(IKernel)).IsNull().Throw();

            this.kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}