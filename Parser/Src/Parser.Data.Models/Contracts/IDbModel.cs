using System;

namespace Parser.Data.Models.Contracts
{
    public interface IDbModel
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
