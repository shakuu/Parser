namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineFactory
    {
        ILogFileParserEngine CreateLogFileParserEngine();
    }
}
