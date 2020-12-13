using AspnetRunBasics.ApiCollection.Infrastructure;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using AspnetRunBasics.Settings;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection
{
    public class BasketApi : BaseHttpClientWithFactory, IBasketApi
    {
        private readonly IApiSettings settings;

        public BasketApi(IHttpClientFactory factory, IApiSettings settings):base(factory)
        {
            this.settings = settings;
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.BasketPath)
                            .AddToPath("Checkout")
                            .HttpMethod(HttpMethod.Post)
                            .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            await SendRequest<BasketModel>(message);
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.BasketPath)
                            .AddQueryString("username",userName)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<BasketModel>(message);
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var message = new HttpRequestBuilder(settings.BaseAddress)
                            .SetPath(settings.BasketPath)
                            .HttpMethod(HttpMethod.Post)
                            .GetHttpMessage();
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<BasketModel>(message);
        }
    }
}
