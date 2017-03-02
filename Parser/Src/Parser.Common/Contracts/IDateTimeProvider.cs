using System;

namespace Parser.Common.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();

        DateTime GetUtcNow();
    }
}
