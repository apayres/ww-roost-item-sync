namespace Roost.ItemSync.ETL.WebServices
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}