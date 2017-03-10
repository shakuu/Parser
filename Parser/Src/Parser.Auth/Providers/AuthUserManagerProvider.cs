using System.Web;

using Microsoft.AspNet.Identity.Owin;

using Parser.Auth.Contracts;
using Parser.Auth.Managers;

namespace Parser.Auth.Providers
{
    public class AuthUserManagerProvider : IAuthUserManagerProvider
    {
        public IAuthUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AuthUserManager>();
            }
        }
    }
}
