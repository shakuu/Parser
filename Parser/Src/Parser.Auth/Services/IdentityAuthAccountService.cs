using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Bytes2you.Validation;

using Parser.Auth.Contracts;

namespace Parser.Auth.Services
{
    public class IdentityAuthAccountService : IIdentityAuthAccountService
    {
        private readonly IAuthSignInManagerProvider authSignInManagerProvider;
        private readonly IAuthUserManagerProvider authUserManagerProvider;

        public IdentityAuthAccountService(IAuthSignInManagerProvider authSignInManagerProvider, IAuthUserManagerProvider authUserManagerProvider)
        {
            Guard.WhenArgument(authSignInManagerProvider, nameof(IAuthSignInManagerProvider)).IsNull().Throw();
            Guard.WhenArgument(authUserManagerProvider, nameof(IAuthUserManagerProvider)).IsNull().Throw();

            this.authSignInManagerProvider = authSignInManagerProvider;
            this.authUserManagerProvider = authUserManagerProvider;
        }

        public async Task<IdentityResult> CreateAsync(AuthUser user, string password)
        {
            return await this.authUserManagerProvider.UserManager.CreateAsync(user, password);
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await this.authSignInManagerProvider.SignInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public async Task SignInAsync(AuthUser user)
        {
            await this.authSignInManagerProvider.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }
    }
}
