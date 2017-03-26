namespace Parser.Common.Utilities.Contracts
{
    public interface IIdentityProvider
    {
        string GetUsername();

        bool IsAuthenticated();

        bool IsInRole(string role);
    }
}
