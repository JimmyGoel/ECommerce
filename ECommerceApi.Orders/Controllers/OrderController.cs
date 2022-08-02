using ECommerceApi.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrderController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerAsync()
        {
            var result = await orderProvider.GetOrderAsync();
            if (result.IsSuccess)
                return Ok(result.orders);
            return NotFound();
        }

        [HttpGet("{CustomerID}")]
        public async Task<IActionResult> GetCustomerAsync(int CustomerID)
        {
            var result = await orderProvider.GetOrderAsync(CustomerID);

            if (result.IsSuccess)
                return Ok(result.orders);
            return NotFound();
        }
    }
}
