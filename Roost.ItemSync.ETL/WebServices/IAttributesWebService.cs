using Roost.ItemSync.ETL.Models.Source;

namespace Roost.ItemSync.ETL.WebServices
{
    public interface IAttributesWebService
    {
        Task<List<ItemAttribute>> GetAttributes(int itemID);
    }
}