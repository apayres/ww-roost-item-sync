using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Roost.ItemSync.ETL.Configuration;
using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly string _connectionString;
        private const string PARTITION_KEY = "ITEM_CATALOG_ENTRY";

        public ItemsRepository(IOptions<ETLOptions> options)
        {
            _connectionString = options.Value.CosmosConnectionString;
        }

        public async Task<Item> Insert(Item item)
        {
            item.PartitionKey = PARTITION_KEY;
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemCatalog");

                item.Id = Guid.NewGuid();
                return await container.CreateItemAsync(item);
            }
        }

        public async Task<Item> Update(Item item)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemCatalog");
                item.PartitionKey = PARTITION_KEY;
                return await container.ReplaceItemAsync(item, item.Id.ToString(), new PartitionKey(PARTITION_KEY));
            }
        }

        public async Task Delete(Item item)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemCatalog");
                await container.DeleteItemAsync<Item>(item.Id.ToString(), new PartitionKey(PARTITION_KEY));
            }
        }

        public async Task<Item> Get(Guid id)
        {
            Item item = null;

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemCatalog");

                using (var iterator = container.GetItemQueryIterator<Item>($"SELECT * FROM c WHERE c.id = '{id.ToString()}'"))
                {
                    while (iterator.HasMoreResults)
                    {
                        var result = await iterator.ReadNextAsync();
                        item = result.FirstOrDefault();
                    }
                }
            }

            return item;
        }

        public async Task<List<Item>> GetAll()
        {
            var items = new List<Item>();

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer("roost-db", "ItemCatalog");
                using (var iterator = container.GetItemQueryIterator<Item>())
                {
                    while (iterator.HasMoreResults)
                    {
                        var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                        items.AddRange(response.ToList());
                    }
                }
            }

            return items;
        }
    }
}
