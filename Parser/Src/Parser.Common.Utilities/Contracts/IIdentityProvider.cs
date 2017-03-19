namespace Parser.Common.Utilities.Contracts
{
    public interface IIdentityProvider
    {
        string Username { get; }

        bool IsAuthenticated { get; }
    }
}
