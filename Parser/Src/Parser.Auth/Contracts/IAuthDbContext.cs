using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

namespace Parser.Auth.Contracts
{
    public interface IAuthDbContext
    {
        IDbSet<AuthUser> Users { get; }

        IDbSet<IdentityRole> Roles { get; }
    }
}
