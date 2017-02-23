using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Parser.ConsoleClient.NinjectModules
{
    internal sealed class NinjectStandardKernelProvider
    {
        public static IKernel Kernel { get; private set; }

        static NinjectStandardKernelProvider()
        {
            NinjectStandardKernelProvider.Kernel = new StandardKernel();
        }
    }
}
