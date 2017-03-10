using System;

using Parser.Data.Models.Contracts;

namespace Parser.Data.Tests.Mocks
{
    public class MockDbModel : IDbModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
