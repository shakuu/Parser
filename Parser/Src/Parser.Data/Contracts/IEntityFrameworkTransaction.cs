using System;

namespace Parser.Data.Contracts
{
    /// <summary>
    /// Unit Of Work is a less catchy name!
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
    public interface IEntityFrameworkTransaction : IDisposable
    {
        void SaveChangesAsync();

        void SaveChanges();
    }
}
