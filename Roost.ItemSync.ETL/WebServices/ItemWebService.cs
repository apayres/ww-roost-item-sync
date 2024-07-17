using Roost.ItemSync.ETL.Models.Source;
using System.Text.Json;

namespace Roost.ItemSync.ETL.WebServices
{
    public class ItemWebService : WebServiceBase, IItemWebService
    {
        public ItemWebService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<List<Item>> GetItems()
        {
            var uri = "item";
            var response = await GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<Item[]>(payload, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return items.ToList();
        }
    }
}
