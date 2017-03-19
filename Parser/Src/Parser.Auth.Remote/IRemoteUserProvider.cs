namespace Parser.Auth.Remote
{
    // TODO: 
    public interface IRemoteUserProvider
    {
        IRemoteUser LoggedInRemoteUser { get; }
    }
}
