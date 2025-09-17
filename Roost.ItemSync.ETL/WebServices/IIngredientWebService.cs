using Roost.ItemSync.ETL.Models.Source;

namespace Roost.ItemSync.ETL.WebServices
{
    public interface IIngredientWebService
    {
        Task<List<Ingredient>> GetRecipe(int itemId);
    }
}