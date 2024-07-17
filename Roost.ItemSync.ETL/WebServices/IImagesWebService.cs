using Roost.ItemSync.ETL.Models.Source;

namespace Roost.ItemSync.ETL.WebServices
{
    public interface IImagesWebService
    {
        Task<List<ItemImage>> GetImages(int itemID);
    }
}