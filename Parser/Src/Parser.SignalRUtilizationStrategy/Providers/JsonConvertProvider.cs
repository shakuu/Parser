using Newtonsoft.Json;

using Parser.SignalRUtilizationStrategy.Contracts;

namespace Parser.SignalRUtilizationStrategy.Providers
{
    public class JsonConvertProvider : IJsonConvertProvider
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
