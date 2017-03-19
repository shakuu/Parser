namespace Parser.Auth.Remote
{
    public interface IRemoteUserLoginService
    {
        void Login(string username, string password);
    }
}
