using Parser.Auth.Remote.Models;

namespace Parser.Auth.Remote.Factories
{
    public interface IRemoteUserFactory
    {
        RemoteUser CreateRemoteUser(string username);
    }
}
