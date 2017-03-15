using System;

namespace Parser.Common.Utilities.Contracts
{
    public interface ICacheProvider
    {
        object this[string key] { get; }

        void Add(string key, object value, DateTime absoluteExpiration);
    }
}
