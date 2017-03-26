using System.Web;

using Parser.Common.Constants.Configuration;
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

        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public bool IsOwner()
        {
            return HttpContext.Current.User.Identity.Name == UserRoles.OwnerAccountName;
        }
    }
}
