using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parser.FileReader.Engines;
using Parser.FileReader.Factories;
using Parser.FileReader.Strategies;

namespace Parser.ConsoleClient
{
    class Program
    {
        private const string MorninWoodDummyParse = @"../../../../../SampleLogs/combat_2017-02-22_22_30_37_978667.txt";

        static void Main(string[] args)
        {
            var strategy = new CommandParsingStrategy(null);
            var engine = new FileReaderEngine(strategy);

            engine.Start(Program.MorninWoodDummyParse);
        }
    }
}
