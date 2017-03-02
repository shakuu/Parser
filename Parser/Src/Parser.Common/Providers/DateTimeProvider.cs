using System;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }

        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
