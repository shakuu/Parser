using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        public async Task<string> PostAsync(string url, IDictionary<string, string> content)
        {
            var client = new HttpClient();
            var formUrlEncodedContent = new FormUrlEncodedContent(content);

            var response = await client.PostAsync(url, formUrlEncodedContent);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
