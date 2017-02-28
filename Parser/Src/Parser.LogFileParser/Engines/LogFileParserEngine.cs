using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        private readonly IParseResultFactory parseResultFactory;

        private readonly Queue<ICommand> commandsQueue;

        public LogFileParserEngine(IParseResultFactory parseResultFactory)
        {
            Guard.WhenArgument(parseResultFactory, nameof(IParseResultFactory)).IsNull().Throw();

            this.parseResultFactory = parseResultFactory;

            this.commandsQueue = new Queue<ICommand>();

            // TODO: Async parsing
        }

        public void EnqueueCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            this.commandsQueue.Enqueue(command);
        }

        public IParseResult GetParseResult()
        {
            // TODO: 
            return this.parseResultFactory.CreateParseResult();
        }
    }
}
