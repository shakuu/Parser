using System.Data.Entity;
using System.Linq;

using Bytes2you.Validation;

using Parser.Auth.Contracts;
using Parser.Auth.ViewModels;
using Parser.Common.Constants.Configuration;

namespace Parser.Auth.Services
{
    public class AuthOwnerService : IAuthOwnerService
    {
        private const int DefaultPageSize = 50;

        private readonly IAuthUserManagerProvider authUserManagerProvider;
        private readonly IRoleIdService roleIdService;

        public AuthOwnerService(IAuthUserManagerProvider authUserManagerProvider, IRoleIdService roleIdService)
        {
            Guard.WhenArgument(authUserManagerProvider, nameof(IAuthUserManagerProvider)).IsNull().Throw();
            Guard.WhenArgument(roleIdService, nameof(IRoleIdService)).IsNull().Throw();

            this.authUserManagerProvider = authUserManagerProvider;
            this.roleIdService = roleIdService;
        }

        public void AddRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByUsername(username);
            this.authUserManagerProvider.UserManager.AddUserToRole(user.Id, UserRoles.AdminRole);
        }

        public void RemoveRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByUsername(username);
            this.authUserManagerProvider.UserManager.RemoveUserFromRole(user.Id, UserRoles.AdminRole);
        }

        public OwnerViewModel GetAuthUsersOnPage(int pageNumber)
        {
            var viewModel = new OwnerViewModel();

            var adminRoleId = this.roleIdService.GetIdForRole(UserRoles.AdminRole);

            viewModel.AuthUsers = this.authUserManagerProvider.UserManager.AuthUsers
                .OrderBy(u => u.UserName)
                .Take(AuthOwnerService.DefaultPageSize * pageNumber)
                .Select(u => new AuthUserViewModel() { Username = u.UserName, IsAdmin = u.Roles.Any(r => r.RoleId == adminRoleId) })
                .ToList();

            viewModel.PageNumber = viewModel.AuthUsers.Count() / AuthOwnerService.DefaultPageSize + 1;

            return viewModel;
        }
    }
}
