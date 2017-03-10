using Microsoft.AspNet.Identity.EntityFramework;

using Parser.Common.Constants.Configuration;

namespace Parser.Auth
{
    public class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext()
            : base(ConnectionStrings.ParserDbConnectionString, throwIfV1Schema: false)
        {
        }

        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}
