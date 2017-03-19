using System.Collections.Generic;

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

        private readonly IHttpClientProvider httpClientProvider;
        private readonly IJsonConvertProvider jsonConvertProvider;
        private readonly IRemoteUserFactory remoteUserFactory;

        public RemoteUserProvider(IHttpClientProvider httpClientProvider, IJsonConvertProvider jsonConvertProvider, IRemoteUserFactory remoteUserFactory)
        {
            Guard.WhenArgument(httpClientProvider, nameof(IHttpClientProvider)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(remoteUserFactory, nameof(IRemoteUserFactory)).IsNull().Throw();

            this.httpClientProvider = httpClientProvider;
            this.jsonConvertProvider = jsonConvertProvider;
            this.remoteUserFactory = remoteUserFactory;

            // TODO: Testing
            this.LoggedInRemoteUser = remoteUserFactory.CreateRemoteUser(RemoteUserProvider.FixedUsernameForTesting);
        }

        public IRemoteUser LoggedInRemoteUser { get; private set; }

        public async void Login(string username, string password)
        {
            var postContent = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            var remoteAuthResult = await this.httpClientProvider.PostAsync(RemoteUserProvider.RemoteUserAuthService, postContent);
            var responseResult = this.jsonConvertProvider.DeserializeObject<RemoteAuthResult>(remoteAuthResult);

            if (!string.IsNullOrEmpty(responseResult.Result))
            {
                this.LoggedInRemoteUser = this.remoteUserFactory.CreateRemoteUser(username);
            }
            else
            {
                this.LoggedInRemoteUser = null;
            }
        }
    }
}
