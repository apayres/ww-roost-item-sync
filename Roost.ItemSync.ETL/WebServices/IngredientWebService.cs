using Roost.ItemSync.ETL.Models.Source;
using System.Text.Json;

namespace Roost.ItemSync.ETL.WebServices
{
    public class IngredientWebService : WebServiceBase, IIngredientWebService
    {
        public IngredientWebService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<List<Ingredient>> GetRecipe(int itemId)
        {
            var uri = "ingredient/Recipe/" + itemId;
            var response = await GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<Ingredient[]>(payload, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return items.ToList();
        }
    }
}
