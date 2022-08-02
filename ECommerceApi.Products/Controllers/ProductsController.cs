using ECommerceApi.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await productsProvider.GetProductAsync();
            if (result.IsSucess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductAsync(int Id)
        {
            var result = await productsProvider.GetProductAsync(Id);
            if (result.IsSucess)
            {
                return Ok(result.product);
            }
            return NotFound();
        }
    }
}
