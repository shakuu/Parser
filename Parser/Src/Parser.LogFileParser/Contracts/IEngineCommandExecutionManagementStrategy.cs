using System.Collections.Generic;

namespace Parser.LogFileParser.Contracts
{
    public interface IEngineCommandExecutionManagementStrategy
    {
        void ManageCommandExecution(IDictionary<string, ILogFileParserEngine> logFileParserEngines);
    }
}
