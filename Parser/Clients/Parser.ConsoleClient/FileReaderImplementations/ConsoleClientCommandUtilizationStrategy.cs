using System;

using Parser.FileReader.Contracts;

namespace Parser.ConsoleClient.FileReaderImplementations
{
    internal class ConsoleClientCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        public void UtilizeCommand(ICommand command)
        {
            Console.WriteLine(command.AbilityActivatorName);
        }
    }
}
