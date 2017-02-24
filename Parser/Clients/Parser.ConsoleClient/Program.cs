using System.Threading;

using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.FileReader.Contracts;

namespace Parser.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<IFileReaderEngine>();

            engine.StartAsync();
            Thread.Sleep(100);
            engine.Stop();
        }
    }
}
