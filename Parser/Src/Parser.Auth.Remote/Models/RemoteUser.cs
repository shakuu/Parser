namespace Parser.Auth.Remote.Models
{
    public class RemoteUser : IRemoteUser
    {
        public RemoteUser(string username)
        {
            this.Username = username;
        }

        public string Username { get; private set; }
    }
}
