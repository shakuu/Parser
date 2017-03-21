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
        private readonly IAuthDbContext authDbContext;

        public AuthOwnerService(IAuthUserManagerProvider authUserManagerProvider, IAuthDbContext authDbContext)
        {
            Guard.WhenArgument(authUserManagerProvider, nameof(IAuthUserManagerProvider)).IsNull().Throw();
            Guard.WhenArgument(authDbContext, nameof(IAuthDbContext)).IsNull().Throw();

            this.authUserManagerProvider = authUserManagerProvider;
            this.authDbContext = authDbContext;
        }

        public void AddRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByUsername(username);
            this.authUserManagerProvider.UserManager.AddUserToRole(user.Id, AuthOwnerService.AdminRole);
        }

        public void RemoveRoleAdmin(string username)
        {
            var user = this.authUserManagerProvider.UserManager.FindByUsername(username);
            this.authUserManagerProvider.UserManager.RemoveUserFromRole(user.Id, AuthOwnerService.AdminRole);
        }

        public OwnerViewModel GetAuthUsersOnPage(int pageNumber)
        {
            var viewModel = new OwnerViewModel();

            var adminRoleId = this.authDbContext.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefault();

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
