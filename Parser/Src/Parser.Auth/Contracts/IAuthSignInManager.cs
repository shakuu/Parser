using System.Threading.Tasks;

using Microsoft.AspNet.Identity.Owin;

namespace Parser.Auth.Contracts
{
    public interface IAuthSignInManager
    {
        Task SignInAsync(AuthUser user, bool isPersistent, bool rememberBrowser);

        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout);
    }
}
