using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Factories;

namespace Parser.LogFile.Parser.Managers
{
    public class LogFileParserEngineManager : ILogFileParserEngineManager
    {
        private const string RequestedEngineNotFoundErrorMessage = "Requested engine not found.";

        private readonly IGuidStringProvider guidStringProvider;
        private readonly ILogFileParserEngineFactory logFileParserEngineFactory;

        private readonly ConcurrentDictionary<string, ILogFileParserEngine> logFileParserEngines;
        private readonly ConcurrentDictionary<string, ConcurrentBag<ILogFileParserEngine>> logFileParserEnginesByUser;
        private readonly ConcurrentDictionary<string, string> usernamesByEngineId;

        public LogFileParserEngineManager(IGuidStringProvider guidStringProvider, ILogFileParserEngineFactory logFileParserEngineFactory)
        {
            Guard.WhenArgument(guidStringProvider, nameof(IGuidStringProvider)).IsNull().Throw();
            Guard.WhenArgument(logFileParserEngineFactory, nameof(ILogFileParserEngineFactory)).IsNull().Throw();

            this.guidStringProvider = guidStringProvider;
            this.logFileParserEngineFactory = logFileParserEngineFactory;

            this.logFileParserEngines = new ConcurrentDictionary<string, ILogFileParserEngine>();
            this.logFileParserEnginesByUser = new ConcurrentDictionary<string, ConcurrentBag<ILogFileParserEngine>>();
            this.usernamesByEngineId = new ConcurrentDictionary<string, string>();
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
                throw new ArgumentException(LogFileParserEngineManager.RequestedEngineNotFoundErrorMessage);
            }
        }

        public string StopLogFileParserEngine(string engineId)
        {
            Guard.WhenArgument(engineId, nameof(engineId)).IsNullOrEmpty().Throw();

            var logFileParserEnginesContainsKey = this.logFileParserEngines.ContainsKey(engineId);
            if (logFileParserEnginesContainsKey)
            {
                ILogFileParserEngine outParameter;
                if (this.logFileParserEngines.TryRemove(engineId, out outParameter))
                {
                    var usernamesByEngineIdContainsEngineId = this.usernamesByEngineId.ContainsKey(engineId);
                    if (usernamesByEngineIdContainsEngineId)
                    {
                        var username = this.usernamesByEngineId[engineId];

                        this.logFileParserEnginesByUser[username].TryTake(out outParameter);
                    }
                }

                return engineId;
            }
            else
            {
                throw new ArgumentException(LogFileParserEngineManager.RequestedEngineNotFoundErrorMessage);
            }
        }

        public string StartLogFileParserEngine(string username)
        {
            var newEngineId = this.guidStringProvider.NewGuidString();
            var newEngine = this.logFileParserEngineFactory.CreateLogFileParserEngine();

            this.logFileParserEngines.TryAdd(newEngineId, newEngine);

            this.AssignNewEngineToUsername(username, newEngineId, newEngine);

            return newEngineId;
        }

        private void AssignNewEngineToUsername(string username, string engineId, ILogFileParserEngine engine)
        {
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            var logFileParserEnginesByUserContainsUsername = this.logFileParserEnginesByUser.ContainsKey(username);
            if (!logFileParserEnginesByUserContainsUsername)
            {
                this.logFileParserEnginesByUser.TryAdd(username, new ConcurrentBag<ILogFileParserEngine>());
            }

            this.logFileParserEnginesByUser[username].Add(engine);
            this.usernamesByEngineId.TryAdd(engineId, username);
        }
    }
}
