namespace Parser.LogFileParser.Contracts
{
    public interface ICommandResolutionHandlerChain
    {
        ICommandResolutionHandler NextCommandResolutionHandler { get; set; }
    }
}
