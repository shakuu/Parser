using System.Threading;

using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.LogFileReader.Contracts;

namespace Parser.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<ILogFileReaderEngine>();

            engine.StartAsync();
            Thread.Sleep(1000);
            engine.Stop();
        }
    }
}
