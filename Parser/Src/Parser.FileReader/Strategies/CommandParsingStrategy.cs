using System;
using System.Linq;

using Bytes2you.Validation;

using Parser.FileReader.Contracts;
using Parser.FileReader.Factories;

namespace Parser.FileReader.Strategies
{
    public class CommandParsingStrategy : ICommandParsingStrategy
    {
        private readonly ICommandFactory commandFactory;

        public CommandParsingStrategy(ICommandFactory commandFactory)
        {
            Guard.WhenArgument(commandFactory, nameof(ICommandFactory)).IsNull().Throw();

            this.commandFactory = commandFactory;
        }

        public ICommand ParseInputCommand(string input)
        {
            var commandWords = input.Split(new[] { '[', ']', '(', ')', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            commandWords = commandWords.Where(s => s != " ").ToArray();

            var nextCommand = this.commandFactory.CreateCommand();

            nextCommand.TimeStamp = DateTime.Parse(commandWords[0]);
            nextCommand.AbilityActivatorName = commandWords[1];
            nextCommand.AbilityTargetName = commandWords[2];
            // TODO: 
            return null;
        }
    }
}
