using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.Factories
{
    public interface ILogFileParserEngineFactory
    {
        ILogFileParserEngine CreateLogFileParserEngine();
    }
}
