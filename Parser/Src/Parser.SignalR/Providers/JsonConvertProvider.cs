using Newtonsoft.Json;

using Parser.SignalR.Contracts;

namespace Parser.SignalR.Providers
{
    public class JsonConvertProvider : IJsonConvertProvider
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
