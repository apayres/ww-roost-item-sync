using Roost.ItemSync.ETL.Models.Source;
using System.Text.Json;

namespace Roost.ItemSync.ETL.WebServices
{
    public class ImagesWebService : WebServiceBase, IImagesWebService
    {
        public ImagesWebService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<ItemImage>> GetImages(int itemID)
        {
            var uri = "ItemImages/" + itemID;
            var response = await GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadAsStringAsync();
            var images = JsonSerializer.Deserialize<ItemImage[]>(payload, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return images.ToList();
        }
    }
}
