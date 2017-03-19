using System.Web;

using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Utilities.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        public string Username
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
    }
}
