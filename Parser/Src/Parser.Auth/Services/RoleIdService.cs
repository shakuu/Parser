using System.Collections.Concurrent;
using System.Linq;

using Bytes2you.Validation;

using Parser.Auth.Contracts;

namespace Parser.Auth.Services
{
    public class RoleIdService : IRoleIdService
    {
        private readonly IAuthDbContext authDbContext;

        private readonly ConcurrentDictionary<string, string> memorizedRoleIds;

        public RoleIdService(IAuthDbContext authDbContext)
        {
            Guard.WhenArgument(authDbContext, nameof(IAuthDbContext)).IsNull().Throw();

            this.authDbContext = authDbContext;

            this.memorizedRoleIds = new ConcurrentDictionary<string, string>();
        }

        public string GetIdForRole(string roleName)
        {
            Guard.WhenArgument(roleName, nameof(roleName)).IsNullOrEmpty().Throw();

            var roleIdIsMemorized = this.memorizedRoleIds.ContainsKey(roleName);
            if (roleIdIsMemorized == false)
            {
                var roleId = this.authDbContext.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefault();
                this.memorizedRoleIds.TryAdd(roleName, roleId);
            }

            return this.memorizedRoleIds[roleName];
        }
    }
}
