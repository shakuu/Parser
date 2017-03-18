using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Factories
{
    public interface ILogFileParserEngineFactory
    {
        ILogFileParserEngine CreateLogFileParserEngine();
    }
}
