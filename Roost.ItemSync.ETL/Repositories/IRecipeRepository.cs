using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Repositories
{
    public interface IRecipeRepository
    {
        Task Delete(Recipe recipe);
        Task<Recipe> GetRecipe(Guid itemId);
        Task<Recipe> Insert(Recipe recipe);
        Task<Recipe> Update(Recipe recipe);
    }
}