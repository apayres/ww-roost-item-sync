using Microsoft.Extensions.Options;

namespace Roost.ItemSync.ETL.Configuration
{
    public class ETLOptions : IOptions<ETLOptions>
    {
        public string CosmosConnectionString { get; set; }

        public string WebServiceBaseUrl { get; set; }

        public ETLOptions Value => this;
    }
}
