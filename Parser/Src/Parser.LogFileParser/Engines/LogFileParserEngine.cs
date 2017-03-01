using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        private readonly ICommandResolutionHandler commandResolutionStrategy;
        private readonly ICombatStatisticsContainer combatStatisticsContainer;
        private readonly ICombatStatisticsFactory combatStatisticsFactory;

        private readonly Queue<ICommand> commandsQueue;

        public LogFileParserEngine(ICommandResolutionHandler commandResolutionStrategy, ICombatStatisticsContainerFactory combatStatisticsContainerFactory, ICombatStatisticsFactory combatStatisticsFactory)
        {
            Guard.WhenArgument(commandResolutionStrategy, nameof(ICommandResolutionHandler)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsContainer, nameof(ICombatStatisticsContainer)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsFactory, nameof(ICombatStatisticsFactory)).IsNull().Throw();

            this.commandResolutionStrategy = commandResolutionStrategy;
            this.combatStatisticsContainer = combatStatisticsContainerFactory.CreateCombatStatisticsContainer();
            this.combatStatisticsFactory = combatStatisticsFactory;

            this.commandsQueue = new Queue<ICommand>();

            // TODO: Async parsing
        }

        public void EnqueueCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            this.commandsQueue.Enqueue(command);
        }

        public ICombatStatisticsContainer GetComabtStatistics()
        {
            // TODO: 
            return this.combatStatisticsContainer;
        }
    }
}
