using Ninject;

using Parser.ConsoleClient.NinjectModules;
using Parser.FileReader.Contracts;

namespace Parser.ConsoleClient
{
    class Program
    {
        private const string MorninWoodDummyParse = @"../../../../../SampleLogs/combat_2017-02-22_22_30_37_978667.txt";

        static void Main(string[] args)
        {
            var engine = NinjectStandardKernelProvider.Kernel.Get<IFileReaderEngine>();

            engine.Start(Program.MorninWoodDummyParse);
        }
    }
}
