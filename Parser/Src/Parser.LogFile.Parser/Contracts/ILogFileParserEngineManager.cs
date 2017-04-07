using System.Collections.Generic;

using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ILogFileParserEngineManager
    {
        string StartLogFileParserEngine(string username);

        string StopLogFileParserEngine(string engineId);

        void EnqueueCommandToEngineWithId(string engineId, ICommand command);

        void EnqueueCommandEnumerationToEngineWithId(string engineId, IEnumerable<ICommand> commandEnumeration);

        ILogFileParserEngine FindLogFileParserEngineByUsername(string username);
    }
}
