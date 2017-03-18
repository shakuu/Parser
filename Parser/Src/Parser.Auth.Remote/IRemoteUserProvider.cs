namespace Parser.Auth.Remote
{
    public interface IRemoteUserProvider
    {
        IRemoteUser LoggedInRemoteUser { get; }
    }
}
