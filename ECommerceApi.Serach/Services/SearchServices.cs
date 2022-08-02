using ECommerceApi.Serach.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Services
{
    public class SearchServices : ISerachServices
    {
        private readonly IOrderServices orderServices;
        private readonly IProductServices productServices;
        private readonly ICustomerService customerService;

        public SearchServices(IOrderServices orderServices,IProductServices productServices,
            ICustomerService customerService)
        {
            this.orderServices = orderServices;
            this.productServices = productServices;
            this.customerService = customerService;
        }
        public async Task<(bool IsSucess, dynamic SearchResult)> GetSearchAsync(int CustomerID)
        {
            //await Task.Delay(1);
            //return (true, new { Message = "Hello Word" });

            var OrderResult = await orderServices.GetOrdersAsync(CustomerID);
            var ProductResult = await productServices.GetProductAsyn();
            var CustomerResult = await customerService.GetCustomersAsync();
            if (OrderResult.IsSuccess)
            {
                foreach (var order in OrderResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = ProductResult.ISSucess ?
                            ProductResult.products.FirstOrDefault(p => p.Id == item.ProductID)?.Name :
                            "Product Information Not Available";
                    }
                }
                var result = new { Orders = OrderResult.orders, 
                    Customer=CustomerResult.customers };
                return (true, result);
            }
            return (false, null);
        }
    }
}
