using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineManager
    {
        string StartNewLogFileParserEngine();

        string EnqueueCommandToEngineWithId(string engineId, ICommand command);
    }
}
