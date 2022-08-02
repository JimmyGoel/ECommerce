using ECommerceApi.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerAsync()
        {
            var result = await customerProvider.GetCustomerAsync();
            if (result.IsSuccess)
                return Ok(result.customers);
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerAsync(int Id)
        {
            var result = await customerProvider.GetCustomerAsync(Id);

            if (result.IsSuccess)
                return Ok(result.customer);
            return NotFound();
        }
    }
}
