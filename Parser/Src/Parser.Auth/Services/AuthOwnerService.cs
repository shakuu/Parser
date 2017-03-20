using Bytes2you.Validation;

using Parser.Auth.Contracts;

namespace Parser.Auth.Services
{
    public class AuthOwnerService : IAuthOwnerService
    {
        private const string AdminRole = "Admin";

        private readonly IAuthUserManagerProvider authUserManagerProvider;

        public AuthOwnerService(IAuthUserManagerProvider authUserManagerProvider)
        {
            Guard.WhenArgument(authUserManagerProvider, nameof(IAuthUserManagerProvider)).IsNull().Throw();

            this.authUserManagerProvider = authUserManagerProvider;
        }

        public void AddRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByName(username);
            this.authUserManagerProvider.UserManager.AddToRole(user.Id, AuthOwnerService.AdminRole);
        }
    }
}
