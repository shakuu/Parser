using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineManager
    {
        string StartNewLogFileParserEngine();

        void EnqueueCommandToEngineWithId(string engineId, ICommand command);
    }
}
