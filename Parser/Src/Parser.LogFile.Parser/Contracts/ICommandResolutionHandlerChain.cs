namespace Parser.LogFile.Parser.Contracts
{
    public interface ICommandResolutionHandlerChain
    {
        ICommandResolutionHandler NextCommandResolutionHandler { get; set; }
    }
}
