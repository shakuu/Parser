using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineManager
    {
        string StartLogFileParserEngine();

        string StopLogFileParserEngine(string engineId);
        
        void EnqueueCommandToEngineWithId(string engineId, ICommand command);
    }
}
