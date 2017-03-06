using Parser.Data.Contracts;

namespace Parser.Data.Factories
{
    public interface IBusinessTransactionFactory
    {
        IBusinessTransaction CreateBusinessTransaction();
    }
}
