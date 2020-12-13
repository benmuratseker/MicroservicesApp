using AspnetRunBasics.ApiCollection.Infrastructure;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using AspnetRunBasics.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection
{
    public class CatalogApi : BaseHttpClientWithFactory, ICatalogApi
    {
        private readonly IApiSettings settings;

        public CatalogApi(IHttpClientFactory factory, IApiSettings settings) : base(factory)
        {
            this.settings = settings;
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.CatalogPath)
                            .HttpMethod(HttpMethod.Post)
                            .GetHttpMessage();

            return await SendRequest<CatalogModel>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.CatalogPath)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.CatalogPath)
                            .AddToPath(id)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<CatalogModel>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.CatalogPath)
                            .AddToPath("GetProductsByCategory")
                            .AddToPath(category)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }
    }
}
