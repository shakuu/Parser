using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Factories;

namespace Parser.LogFileParser.Managers
{
    public class LogFileParserEngineManager : ILogFileParserEngineManager
    {
        private readonly IGuidStringProvider guidStringProvider;
        private readonly ILogFileParserEngineFactory logFileParserEngineFactory;

        private readonly IDictionary<string, ILogFileParserEngine> logFileParserEngines;

        public LogFileParserEngineManager(IGuidStringProvider guidStringProvider, ILogFileParserEngineFactory logFileParserEngineFactory)
        {
            Guard.WhenArgument(guidStringProvider, nameof(IGuidStringProvider)).IsNull().Throw();
            Guard.WhenArgument(logFileParserEngineFactory, nameof(ILogFileParserEngineFactory)).IsNull().Throw();

            this.guidStringProvider = guidStringProvider;
            this.logFileParserEngineFactory = logFileParserEngineFactory;

            this.logFileParserEngines = new ConcurrentDictionary<string, ILogFileParserEngine>();
        }

        protected IDictionary<string, ILogFileParserEngine> LogFileParserEngines { get { return this.logFileParserEngines; } }

        public void EnqueueCommandToEngineWithId(string engineId, ICommand command)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            var logFileParserEnginesContainsKey = this.logFileParserEngines.ContainsKey(engineId);
            if (logFileParserEnginesContainsKey)
            {
                var requestedEngine = this.logFileParserEngines[engineId];
                requestedEngine.EnqueueCommand(command);
            }
            else
            {
                throw new ArgumentException("Requested engine not found.");
            }
        }

        public string StopLogFileParserEngine(string engineId)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();

            var logFileParserEnginesContainsKey = this.logFileParserEngines.ContainsKey(engineId);
            if (logFileParserEnginesContainsKey)
            {
                this.logFileParserEngines.Remove(engineId);

                return engineId;
            }
            else
            {
                throw new ArgumentException("Requested engine not found.");
            }
        }

        public string StartLogFileParserEngine()
        {
            var newEngineId = this.guidStringProvider.NewGuidString();
            var newEngine = this.logFileParserEngineFactory.CreateLogFileParserEngine();

            this.logFileParserEngines.Add(newEngineId, newEngine);

            return newEngineId;
        }
    }
}
