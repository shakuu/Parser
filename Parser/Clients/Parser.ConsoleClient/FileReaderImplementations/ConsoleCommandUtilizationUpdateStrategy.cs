using System;

using Parser.LogFile.Reader.Contracts;

namespace Parser.ConsoleClient.FileReaderImplementations
{
    public class ConsoleCommandUtilizationUpdateStrategy : ICommandUtilizationUpdateStrategy
    {
        public void DisplayUpdate(string update)
        {
            Console.WriteLine(update);
        }
    }
}
