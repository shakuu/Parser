using System.Collections.Generic;
using System.Net.Http;

using Bytes2you.Validation;

using Parser.Auth.Remote.Factories;
using Parser.Auth.Remote.Models;
using Parser.Common.Contracts;

namespace Parser.Auth.Remote.Providers
{
    public class RemoteUserProvider : IRemoteUserProvider, IRemoteUserLoginService
    {
        private const string RemoteUserAuthService = "http://localhost:50800/remote";
        private const string FixedUsernameForTesting = "myuser@user.com";

        private readonly IJsonConvertProvider jsonConvertProvider;
        private readonly IRemoteUserFactory remoteUserFactory;

        private IRemoteUser loggedInRemoteUser;

        public RemoteUserProvider(IJsonConvertProvider jsonConvertProvider, IRemoteUserFactory remoteUserFactory)
        {
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(remoteUserFactory, nameof(IRemoteUserFactory)).IsNull().Throw();

            this.jsonConvertProvider = jsonConvertProvider;
            this.remoteUserFactory = remoteUserFactory;

            // TODO: Testing
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
                Guard.WhenArgument(value, nameof(this.LoggedInRemoteUser)).IsNull().Throw();

                this.loggedInRemoteUser = value;
            }
        }

        public async void Login(string username, string password)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>() {
                { "username", username },
                { "password", password }
            });

            var response = await client.PostAsync(RemoteUserProvider.RemoteUserAuthService, content);
            var remoteAuthResult = await response.Content.ReadAsStringAsync();
            var responseResult = this.jsonConvertProvider.DeserializeObject<RemoteAuthResult>(remoteAuthResult);
        }
    }
}
