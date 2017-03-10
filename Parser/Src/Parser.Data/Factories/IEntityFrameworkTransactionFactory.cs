using Parser.Data.Contracts;

namespace Parser.Data.Factories
{
    public interface IEntityFrameworkTransactionFactory
    {
        IEntityFrameworkTransaction CreateEntityFrameworkTransaction();
    }
}
