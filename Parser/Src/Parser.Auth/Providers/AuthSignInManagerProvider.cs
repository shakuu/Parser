using System.Web;

using Microsoft.AspNet.Identity.Owin;

using Parser.Auth.Contracts;
using Parser.Auth.Managers;

namespace Parser.Auth.Providers
{
    public class AuthSignInManagerProvider : IAuthSignInManagerProvider
    {
        public IAuthSignInManager SignInManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<AuthSignInManager>();
            }
        }
    }
}
