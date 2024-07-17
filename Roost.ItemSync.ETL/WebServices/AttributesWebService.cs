using Roost.ItemSync.ETL.Models.Source;
using System.Text.Json;

namespace Roost.ItemSync.ETL.WebServices
{
    public class AttributesWebService : WebServiceBase, IAttributesWebService
    {
        public AttributesWebService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<ItemAttribute>> GetAttributes(int itemID)
        {
            var uri = "ItemAttribute/ByItem/" + itemID;
            var response = await GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadAsStringAsync();
            var attributes = JsonSerializer.Deserialize<ItemAttribute[]>(payload, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return attributes.ToList();
        }
    }
}
