using Parser.LogFileReader.Contracts;

namespace Parser.ConsoleClient.FileReaderImplementations
{
    public class TestingLogFilePathDiscoveryStrategy : ILogFilePathDiscoveryStrategy
    {
        private const string MorninWoodDummyParse = @"../../../../../SampleLogs/combat_2017-02-22_22_30_37_978667.txt";
        private const string SampleParse = @"../../../../../SampleLogs/sample.txt";

        public string DiscoverLogFile()
        {
            return TestingLogFilePathDiscoveryStrategy.SampleParse;
        }
    }
}
