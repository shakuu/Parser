using System.Threading;

using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.FileReader.Contracts;

namespace Parser.ConsoleClient
{
    public class Program
    {
        private const string MorninWoodDummyParse = @"../../../../../SampleLogs/combat_2017-02-22_22_30_37_978667.txt";

        public static void Main()
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<IFileReaderEngine>();

            engine.StartAsync();

            Thread.Sleep(100);
            engine.Stop();
        }
    }
}
