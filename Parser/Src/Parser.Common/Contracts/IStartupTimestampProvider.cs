using System;

namespace Parser.Common.Contracts
{
    public interface IStartupTimestampProvider
    {
        DateTime LatestStartupTimestamp { get; }
    }
}
