using System.Collections.Generic;
using System.Threading.Tasks;

using Bytes2you.Validation;

using Parser.Auth.Remote.Factories;
using Parser.Auth.Remote.Models;
using Parser.Common.Contracts;

namespace Parser.Auth.Remote.Services
{
    public class RemoteUserService : IRemoteUserService, IRemoteUserProvider, IRemoteUserLoginService
    {
        private const string RemoteUserAuthService = "http://parser-mvc.azurewebsites.net/remote";

        private readonly IHttpClientProvider httpClientProvider;
        private readonly IJsonConvertProvider jsonConvertProvider;
        private readonly IRemoteUserFactory remoteUserFactory;

        public RemoteUserService(IHttpClientProvider httpClientProvider, IJsonConvertProvider jsonConvertProvider, IRemoteUserFactory remoteUserFactory)
        {
            Guard.WhenArgument(httpClientProvider, nameof(IHttpClientProvider)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(remoteUserFactory, nameof(IRemoteUserFactory)).IsNull().Throw();

            this.httpClientProvider = httpClientProvider;
            this.jsonConvertProvider = jsonConvertProvider;
            this.remoteUserFactory = remoteUserFactory;
        }

        private IRemoteUser LoggedInRemoteUser { get; set; }

        public IRemoteUser GetLoggedInRemoteUser()
        {
            return this.LoggedInRemoteUser;
        }

        public async Task Login(string username, string password)
        {
            var postContent = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            var remoteAuthResult = await this.httpClientProvider.PostAsync(RemoteUserService.RemoteUserAuthService, postContent);
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
