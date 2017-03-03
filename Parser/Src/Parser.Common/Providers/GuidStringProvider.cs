using System;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class GuidStringProvider : IGuidStringProvider
    {
        public string NewGuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
