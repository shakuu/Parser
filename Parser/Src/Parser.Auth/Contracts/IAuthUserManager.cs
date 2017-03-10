using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace Parser.Auth.Contracts
{
    public interface IAuthUserManager
    {
        Task<IdentityResult> CreateAsync(AuthUser user, string password);
    }
}
