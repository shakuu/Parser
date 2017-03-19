namespace Parser.Auth.Remote
{
    // TODO: 
    public interface IRemoteUserProvider : IRemoteUserLoginService
    {
        IRemoteUser LoggedInRemoteUser { get; }
    }
}
