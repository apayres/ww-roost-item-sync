using System.Net.Http.Headers;

namespace Roost.ItemSync.ETL.WebServices
{
    public abstract class WebServiceBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebServiceBase(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var httpClient = _httpClientFactory.GetHttpClient();
            return await httpClient.GetAsync(uri);
        }
    }
}
