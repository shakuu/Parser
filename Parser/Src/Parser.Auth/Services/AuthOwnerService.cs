using System.Linq;

using Bytes2you.Validation;
using Microsoft.AspNet.Identity.EntityFramework;
using Parser.Auth.Contracts;

namespace Parser.Auth.Services
{
    public class AuthOwnerService : IAuthOwnerService
    {
        private const string AdminRole = "Admin";

        private readonly IAuthDbContext authDbContext;

        public AuthOwnerService(IAuthDbContext authDbContext)
        {
            Guard.WhenArgument(authDbContext, nameof(IAuthDbContext)).IsNull().Throw();

            this.authDbContext = authDbContext;
        }

        public void AddRoleAdmin(string username)
        {
            var role = this.authDbContext.Roles.FirstOrDefault(r => r.Name == AuthOwnerService.AdminRole);
            if (role == null)
            {
                role = new IdentityRole();
                role.Name = AuthOwnerService.AdminRole;

                this.authDbContext.Roles.Add(role);
            }

            var user = this.authDbContext.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                //user.Roles.Add();
            }
        }
    }
}
