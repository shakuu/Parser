using Parser.Auth.Remote.Factories;

namespace Parser.Auth.Remote.Providers
{
    public class RemoteUserProvider : IRemoteUserProvider
    {
        private const string FixedUsernameForTesting = "some@user.com";

        private IRemoteUser loggedInRemoteUser;

        public RemoteUserProvider(IRemoteUserFactory remoteUserFactory)
        {
            this.loggedInRemoteUser = remoteUserFactory.CreateRemoteUser(RemoteUserProvider.FixedUsernameForTesting);
        }

        public IRemoteUser LoggedInRemoteUser
        {
            get
            {
                return this.loggedInRemoteUser;
            }

            private set
            {
                this.loggedInRemoteUser = value;
            }
        }
    }
}
