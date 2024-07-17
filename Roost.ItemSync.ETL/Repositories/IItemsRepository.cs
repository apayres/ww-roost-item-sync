using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Repositories
{
    public interface IItemsRepository
    {
        Task Delete(Item item);
        Task<Item> Get(Guid id);
        Task<List<Item>> GetAll();
        Task<Item> Insert(Item item);
        Task<Item> Update(Item item);
    }
}