using System.Threading.Tasks;

namespace Parser.Data.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
