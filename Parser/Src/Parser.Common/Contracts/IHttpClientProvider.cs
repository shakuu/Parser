using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.Common.Contracts
{
    public interface IHttpClientProvider
    {
        Task<string> PostAsync(string url, IDictionary<string, string> content);
    }
}
