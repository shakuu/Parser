namespace Parser.Data.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        T Create(T entity);
    }
}
