using Microsoft.AspNet.Identity.EntityFramework;

namespace Parser.Auth
{
    public class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}
