using System;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
