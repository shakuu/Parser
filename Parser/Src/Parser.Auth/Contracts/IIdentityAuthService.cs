using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Parser.Auth.Contracts
{
    public interface IIdentityAuthService
    {
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task<IdentityResult> CreateAsync(string email, string password);
    }
}
