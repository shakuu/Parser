using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineService
    {
        string StartNewLogFileParserEngine();

        string EnqueueCommandToEngineWithId(string engineId, ICommand command);
    }
}
