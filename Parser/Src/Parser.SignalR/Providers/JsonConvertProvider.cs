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
            // TODO: Return null on unable to deserialize
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
