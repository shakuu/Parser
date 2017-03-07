using System.Web;

using Parser.HttpContextUtilities.Contracts;

namespace Parser.HttpContextUtilities.Providers
{
    public class HttpContextCacheProvider : IHttpContextCacheProvider
    {
        public object this[string index]
        {
            get
            {
                return HttpContext.Current.Cache[index];
            }

            set
            {
                HttpContext.Current.Cache[index] = value;
            }
        }

        public void Add(string key, object value)
        {
            HttpContext.Current.Cache[key] = value;
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
