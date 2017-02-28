using System;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngineService
    {
        Guid StartNewLogFileParserEngine();
    }
}
