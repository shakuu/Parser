using System;

using Ninject.Extensions.Interception;

namespace Parser.ConsoleClient.FileReaderImplementations
{
    public class LogFilePathDiscoveryStrategyTestingInterceptor : IInterceptor
    {
        private const string MorninWoodDummyParse = @"../../../../../SampleLogs/combat_2017-02-22_22_30_37_978667.txt";
        private const string SampleParse = @"../../../../../SampleLogs/sample.txt";

        public void Intercept(IInvocation invocation)
        {
            try
            {
                // Invoking method for debugging.
                invocation.Proceed();
            }
            catch (Exception)
            {

            }

            // Returning sample data for controlled input.
            invocation.ReturnValue = LogFilePathDiscoveryStrategyTestingInterceptor.MorninWoodDummyParse;
        }
    }
}
