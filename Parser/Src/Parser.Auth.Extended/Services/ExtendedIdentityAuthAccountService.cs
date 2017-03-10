using System;
using System.Threading.Tasks;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Contracts;

namespace Parser.Auth.Extended.Services
{
    public class ExtendedIdentityAuthAccountService : IExtendedIdentityAuthAccountService, IIdentityAuthAccountService
    {
        public Task<Microsoft.AspNet.Identity.IdentityResult> CreateAsync(AuthUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Microsoft.AspNet.Identity.Owin.SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(AuthUser user)
        {
            throw new NotImplementedException();
        }
    }
}
