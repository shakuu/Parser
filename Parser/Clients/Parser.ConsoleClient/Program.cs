using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.LogFile.Reader.Contracts;

namespace Parser.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<ILogFileReaderEngine>();

            engine.StartAsync();

            while (true)
            {

            }
        }
    }
}
