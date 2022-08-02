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
    public class OrderServices : IOrderServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderServices> ilogger;

        public OrderServices(IHttpClientFactory httpClientFactory, ILogger<OrderServices> ilogger)
        {
            this.httpClientFactory = httpClientFactory;
            this.ilogger = ilogger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> orders, string ErrorMessage)>
            GetOrdersAsync(int CustomerID)
        {
            try
            {


                var client = httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/orders/{CustomerID}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, option);

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
