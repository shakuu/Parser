using System;

namespace Parser.Data.Contracts
{
    /// <summary>
    /// Unit Of Work
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
    public interface IBusinessTransaction : IDisposable
    {
        void CommitAsync();

        void Commit();
    }
}
