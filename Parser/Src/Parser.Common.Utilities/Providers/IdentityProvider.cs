using System.Web;

using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Utilities.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        public string GetUsername()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}
