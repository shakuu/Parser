using Newtonsoft.Json;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class JsonConvertProvider : IJsonConvertProvider
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
