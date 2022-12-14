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
    public class CustomerService:ICustomerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> ilogger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> ilogger)
        {
            this.httpClientFactory = httpClientFactory;
            this.ilogger = ilogger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customers");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Customer>>(content, option);

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
