using AutoMapper;
using ECommerceApi.Customers.Db;
using ECommerceApi.Customers.Interfaces;
using ECommerceApi.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Customers.Providers
{
    public class CustomersProviders:ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomersProviders> logger;
        private readonly IMapper mapper;

        public CustomersProviders(CustomerDbContext dbContext,
            ILogger<CustomersProviders> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.customers.Any())
            {
                dbContext.customers.Add(new Db.Customer() { Id = 1, Name = "Jimmy", Address ="#123"});
                dbContext.customers.Add(new Db.Customer() { Id = 2, Name = "Abhai", Address = "#14623" });
                dbContext.customers.Add(new Db.Customer() { Id = 3, Name = "Randhir",Address ="#12973" });
                dbContext.customers.Add(new Db.Customer() { Id = 4, Name = "Mohan", Address = "#132123" });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {
                logger?.LogInformation("Querying Customers");
                var customers = await dbContext.customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
                throw;
            }
            
            
        }

        public async Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int Id)
        {
            try
            {
                logger?.LogInformation("Querying Customer");
                var customers = await dbContext.customers.FirstOrDefaultAsync(P=>P.Id==Id);
                if (customers != null )
                {
                    logger?.LogInformation($"{customers} customer(s) found");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
                throw;
            }
        }
    }
}
