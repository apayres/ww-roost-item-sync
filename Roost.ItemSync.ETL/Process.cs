using Microsoft.Extensions.Logging;
using Roost.ItemSync.ETL.Repositories;
using Roost.ItemSync.ETL.Utilities;
using Roost.ItemSync.ETL.WebServices;

namespace Roost.ItemSync.ETL
{
    public class Process : IProcess
    {
        private readonly IItemsRepository _itemRepository;
        private readonly IItemWebService _itemWebService;
        private readonly IIngredientWebService _ingredientWebService;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogger<Process> _logger;

        public Process(IItemsRepository itemRepository, IItemWebService itemWebService, IIngredientWebService ingredientWebService, IRecipeRepository recipeRepository, ILogger<Process> logger)
        {
            _itemRepository = itemRepository;
            _itemWebService = itemWebService;
            _ingredientWebService = ingredientWebService;
            _recipeRepository = recipeRepository;
            _logger = logger;
        }

        public async Task SyncItems()
        {
            _logger.LogInformation("Getting Source Items");
            var items = await _itemWebService.GetItems();

            _logger.LogInformation("Getting Existing Items");
            var existingItems = await _itemRepository.GetAll();

            _logger.LogInformation("Performing Item Sync");
            await SyncItems(items, existingItems);

            _logger.LogInformation("Performing Recipe Sync");
            existingItems = await _itemRepository.GetAll();
            await SyncRecipes(existingItems);
        }

        private async Task SyncItems(List<Models.Source.Item> items, List<Models.Target.Item> existingItems)
        {
            var itemsDeleted = await DeleteRemovedItems(items, existingItems);
            _logger.LogInformation($"Deleted items: {itemsDeleted}");

            var updatedItems = await UpdateExistingItems(items, existingItems);
            _logger.LogInformation($"Updated items: {updatedItems}");

            var insertedItems = await InsertNewItems(items, existingItems);
            _logger.LogInformation($"Inserted items: {insertedItems}");
        }

        private async Task<int> DeleteRemovedItems(List<Models.Source.Item> items, List<Models.Target.Item> existingItems)
        {
            var itemsDeleted = 0;
            var itemsToDelete = existingItems.Where(x => !items.Any(y => y.Upc == x.Upc));

            foreach (var item in itemsToDelete)
            {
                try
                {
                    await _itemRepository.Delete(item);
                    itemsDeleted++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Could not deleted item ${item.Upc}: {ex.Message}");
                }
            }

            return itemsDeleted;
        }

        private async Task<int> InsertNewItems(List<Models.Source.Item> items, List<Models.Target.Item> existingItems)
        {
            var itemsToInsert = items.Where(x => !existingItems.Any(y => y.Upc == x.Upc));
            if (!itemsToInsert.Any())
            {
                return 0;
            }

            var itemsInserted = 0;
            foreach (var item in itemsToInsert)
            {
                try
                {
                    var itemToInsert = ItemMapper.MapToTarget(item);
                    await _itemRepository.Insert(itemToInsert);
                    itemsInserted++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Could not insert item ${item.Upc}: {ex.Message}");
                }
            }

            return itemsInserted;
        }

        private async Task<int> UpdateExistingItems(List<Models.Source.Item> items, List<Models.Target.Item> existingItems)
        {
            var itemsUpdated = 0;

            foreach (var item in items)
            {
                var matchingItem = existingItems.FirstOrDefault(x => x.Upc == item.Upc);
                if (matchingItem == null)
                {
                    continue;
                }

                try
                {
                    var doesItemNeedUpdating = !ItemComparer.DoItemsMatch(item, matchingItem);
                    if (doesItemNeedUpdating)
                    {
                        var replacement = ItemMapper.MapToTarget(item);
                        replacement.Id = matchingItem.Id;

                        await _itemRepository.Update(replacement);
                        itemsUpdated++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Could not update item ${item.Upc}: {ex.Message}");
                }
            }

            return itemsUpdated;
        }

        private async Task SyncRecipes(List<Models.Target.Item> items)
        {
            foreach(var item in items)
            {
                await SyncRecipe(item);
            }
        }

        private async Task SyncRecipe(Models.Target.Item item)
        {
            var ingredients = await _ingredientWebService.GetRecipe(item.OriginalItemId);
            var recipe = await _recipeRepository.GetRecipe(item.Id);

            if (recipe == null && ingredients == null || !ingredients.Any())
            {
                return;
            }

            if(recipe == null && ingredients != null && ingredients.Any())
            {
                await _recipeRepository.Insert(new Models.Target.Recipe()
                {
                    Ingredients = ingredients.Select(x => IngredientMapper.MapToTarget(x)).ToList(),
                    ItemId = item.Id
                });

                return;
            }

            if(recipe != null && ingredients == null || !ingredients.Any())
            {
                await _recipeRepository.Delete(recipe);
                return;
            }

            recipe.Ingredients = ingredients.Select(x => IngredientMapper.MapToTarget(x)).ToList();
            await _recipeRepository.Update(recipe);
        }
    }
}
