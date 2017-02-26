using System.Threading;

using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.LogFileReader.Contracts;

namespace Parser.ConsoleClient
{
    public class Program
    {
        private const string Url = "http://localhost:52589";

        public static void Main()
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<ILogFileReaderEngine>();

            engine.StartAsync();
            //Thread.Sleep(1000);
            //engine.Stop();

            while (true)
            {

            }
        }
    }
}
