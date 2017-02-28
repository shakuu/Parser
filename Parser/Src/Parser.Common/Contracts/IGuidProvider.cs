using System;

namespace Parser.Common.Contracts
{
    public interface IGuidProvider
    {
        Guid NewGuid();
    }
}
