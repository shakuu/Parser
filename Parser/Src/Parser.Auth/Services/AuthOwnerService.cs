using System.Data.Entity;
using System.Linq;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;

namespace Parser.Auth.Services
{
    public class AuthOwnerService : IAuthOwnerService
    {
        private const int DefaultPageSize = 50;

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

        public OwnerViewModel GetAuthUsersOnPage(int pageNumber)
        {
            var viewModel = new OwnerViewModel();

            viewModel.AuthUsers = this.authUserManagerProvider.UserManager.AuthUsers
                .OrderBy(u => u.UserName)
                .Take(AuthOwnerService.DefaultPageSize * pageNumber)
                .Select(u => new AuthUserViewModel() { Username = u.UserName })
                .ToList();

            viewModel.PageNumber = viewModel.AuthUsers.Count() / AuthOwnerService.DefaultPageSize + 1;

            return viewModel;
        }
    }
}
