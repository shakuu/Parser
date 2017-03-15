using System;
using System.Web;
using System.Web.Caching;

using Bytes2you.Validation;

using Parser.Common.Utilities.Contracts;

namespace Parser.Common.Utilities.Providers
{
    public class CacheProvider : ICacheProvider
    {
        public object this[string key]
        {
            get
            {
                return HttpContext.Current.Cache[key];
            }
        }

        public void Add(string key, object value, DateTime absoluteExpiration)
        {
            Guard.WhenArgument(key, nameof(key)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(value, nameof(value)).IsNull().Throw();
            Guard.WhenArgument(absoluteExpiration, nameof(absoluteExpiration)).IsEqual(default(DateTime)).Throw();

            HttpContext.Current.Cache.Add(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
    }
}
