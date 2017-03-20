using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace Parser.Auth.Contracts
{
    public interface IAuthUserManager
    {
        Task<IdentityResult> CreateAsync(AuthUser user, string password);

        AuthUser FindByName(string userName);

        IdentityResult AddToRole(string userId, string role);

        IQueryable<AuthUser> AuthUsers { get; }
    }
}
