using Microsoft.Extensions.Options;
using Roost.ItemSync.ETL.Configuration;

namespace Roost.ItemSync.ETL.WebServices
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private static HttpClient _httpClient = null;

        public HttpClientFactory(IOptions<ETLOptions> options)
        {
            InitializeHttpClient(options.Value.WebServiceBaseUrl);
        }

        public HttpClient GetHttpClient() => _httpClient;

        private static void InitializeHttpClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Roost Item Sync");
        }
    }
}
