using Ninject;

namespace Parser.ConsoleClient.NinjectModules
{
    internal sealed class NinjectStandardKernelProvider
    {
        public static IKernel Kernel { get; private set; }

        static NinjectStandardKernelProvider()
        {
            NinjectStandardKernelProvider.Kernel = new StandardKernel();
            NinjectStandardKernelProvider.Kernel.Load(new LogFileReaderNinjectModule());
            NinjectStandardKernelProvider.Kernel.Load(new SignalRNinjectModule());
            NinjectStandardKernelProvider.Kernel.Load(new CommonNinjectModule());
        }
    }
}
