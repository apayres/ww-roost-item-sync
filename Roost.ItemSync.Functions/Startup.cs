using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Roost.ItemSync.ETL;

[assembly: FunctionsStartup(typeof(Roost.ItemSync.Functions.Startup))]

namespace Roost.ItemSync.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            builder.Services.AddETLServices((x) =>
            {
                x.WebServiceBaseUrl = builder.GetContext().Configuration["ETLOptions:WebServiceBaseUrl"];
                x.CosmosConnectionString = builder.GetContext().Configuration["ETLOptions:CosmosConnectionString"];
            });
        }
    }
}
