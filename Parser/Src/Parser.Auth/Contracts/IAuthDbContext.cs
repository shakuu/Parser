using System.Data.Entity;

namespace Parser.Auth.Contracts
{
    public interface IAuthDbContext
    {
        IDbSet<AuthUser> Users { get; }
    }
}
