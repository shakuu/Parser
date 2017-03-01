using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        private readonly ICombatStatisticsContainerFactory parseResultFactory;

        private readonly Queue<ICommand> commandsQueue;

        public LogFileParserEngine(ICombatStatisticsContainerFactory parseResultFactory)
        {
            Guard.WhenArgument(parseResultFactory, nameof(ICombatStatisticsContainerFactory)).IsNull().Throw();

            this.parseResultFactory = parseResultFactory;

            this.commandsQueue = new Queue<ICommand>();

            // TODO: Async parsing
        }

        public void EnqueueCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            this.commandsQueue.Enqueue(command);
        }

        public IEnumerable<ICombatStatisticsContainer> GetComabtStatistics()
        {
            // TODO: 
            return new[] { this.parseResultFactory.CreateParseResult() };
        }
    }
}
