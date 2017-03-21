namespace Parser.Common.Utilities.Contracts
{
    public interface IIdentityProvider
    {
        string GetUsername();

        bool IsAuthenticated();
    }
}
