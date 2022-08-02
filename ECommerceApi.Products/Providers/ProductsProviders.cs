using AutoMapper;
using ECommerceApi.Products.Db;
using ECommerceApi.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Products.Providers
{
    public class ProductsProviders : IProductsProvider
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<ProductsProviders> logger;
        private readonly IMapper mapper;

        public ProductsProviders(ProductDbContext dbContext, ILogger<ProductsProviders> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 10, Inventory = 50 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 20, Inventory = 20 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Moniter", Price = 25, Inventory = 30 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "Printer", Price = 280, Inventory = 40 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucess, IEnumerable<Models.Product> products, 
            string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucess, Models.Product product, string ErrorMessage)> GetProductAsync(int Id)
        {
            try
            {
                var products = await dbContext.Products.FirstOrDefaultAsync(P=>P.Id==Id);
                if (products != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    } 
}
