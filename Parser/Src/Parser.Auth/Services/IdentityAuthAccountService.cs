using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Parser.Auth.Contracts;
using Parser.Auth.Managers;

namespace Parser.Auth.Services
{
    public class IdentityAuthAccountService : IIdentityAuthAccountService
    {
        private AuthSignInManager _signInManager;
        private AuthUserManager _userManager;

        public AuthSignInManager SignInManager
        {
            get
            {
                // Request scope
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<AuthSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AuthUserManager UserManager
        {
            get
            {
                // Request scope
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<AuthUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<IdentityResult> CreateAsync(AuthUser user, string password)
        {
            return await UserManager.CreateAsync(user, password);
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this.SignInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public async Task SignInAsync(AuthUser user)
        {
            await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }
    }
}
