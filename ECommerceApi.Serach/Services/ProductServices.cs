using ECommerceApi.Serach.Interfaces;
using ECommerceApi.Serach.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductServices> ilogger;

        public ProductServices(IHttpClientFactory httpClientFactory, ILogger<ProductServices> ilogger)
        {
            this.httpClientFactory = httpClientFactory;
            this.ilogger = ilogger;
        }
        public async Task<(bool ISSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsyn()
        {
            try
            {

                var client = httpClientFactory.CreateClient("ProductService");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, option);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                ilogger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
