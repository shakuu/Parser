using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Parser.Auth.Contracts
{
    public interface IIdentityAuthAccountService
    {
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task<IdentityResult> CreateAsync(AuthUser user, string password);

        Task SignInAsync(AuthUser user);
    }
}
