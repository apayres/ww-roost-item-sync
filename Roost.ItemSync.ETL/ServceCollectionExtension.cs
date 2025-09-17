using Microsoft.Extensions.DependencyInjection;
using Roost.ItemSync.ETL.Configuration;
using Roost.ItemSync.ETL.Repositories;
using Roost.ItemSync.ETL.WebServices;

namespace Roost.ItemSync.ETL
{
    public static class ServceCollectionExtension
    {
        public static IServiceCollection AddETLServices(this IServiceCollection services, Action<ETLOptions> options)
        {
            services.Configure<ETLOptions>(options);
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
            services.AddSingleton<IItemsRepository, ItemsRepository>();
            services.AddSingleton<IImagesWebService, ImagesWebService>();
            services.AddSingleton<IAttributesWebService, AttributesWebService>();
            services.AddSingleton<IItemWebService,  ItemWebService>();
            services.AddSingleton<IIngredientWebService, IngredientWebService>();
            services.AddSingleton<IRecipeRepository, RecipeRepository>();
            services.AddSingleton<IProcess, Process>();
            return services;
        }
    }
}
