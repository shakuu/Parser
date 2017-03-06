using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Data.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        TEntity Create(TEntity entity);
    }
}
