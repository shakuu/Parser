using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class StartupTimestampProvider : IStartupTimestampProvider
    {
        public StartupTimestampProvider(IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.LatestStartupTimestamp = dateTimeProvider.GetUtcNow();
        }

        public DateTime LatestStartupTimestamp { get; private set; }
    }
}
