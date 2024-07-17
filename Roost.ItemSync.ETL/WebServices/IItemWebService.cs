using Roost.ItemSync.ETL.Models.Source;

namespace Roost.ItemSync.ETL.WebServices
{
    public interface IItemWebService
    {
        Task<List<Item>> GetItems();
    }
}