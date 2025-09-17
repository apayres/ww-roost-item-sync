using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Roost.ItemSync.ETL.Configuration;
using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;

        public RecipeRepository(IOptions<ETLOptions> options)
        {
            _connectionString = options.Value.CosmosConnectionString;
        }

        public async Task<Recipe> Insert(Recipe recipe)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemRecipes");

                recipe.Id = Guid.NewGuid();
                return await container.CreateItemAsync(recipe);
            }
        }

        public async Task<Recipe> Update(Recipe recipe)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemRecipes");
                return await container.ReplaceItemAsync(recipe, recipe.Id.ToString(), new PartitionKey(recipe.Id.ToString()));
            }
        }

        public async Task Delete(Recipe recipe)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemRecipes");
                await container.DeleteItemAsync<Item>(recipe.Id.ToString(), new PartitionKey(recipe.Id.ToString()));
            }
        }

        public async Task<Recipe> GetRecipe(Guid itemId)
        {
            Recipe recipe = null;

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemRecipes");

                using (var iterator = container.GetItemQueryIterator<Recipe>($"SELECT * FROM c WHERE c.ItemId = '{itemId.ToString()}'"))
                {
                    while (iterator.HasMoreResults)
                    {
                        var result = await iterator.ReadNextAsync();
                        recipe = result.FirstOrDefault();
                    }
                }
            }

            return recipe;
        }

    }
}
