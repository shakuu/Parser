using System;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class GuidStringProvider : IGuidStringProvider
    {
        public string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
