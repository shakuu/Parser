using System;
using System.Collections.Generic;

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

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeCommandEnumeration(IEnumerable<ICommand> commands)
        {
            throw new NotImplementedException();
        }
    }
}
