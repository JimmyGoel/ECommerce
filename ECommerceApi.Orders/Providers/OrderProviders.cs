using AutoMapper;
using ECommerceApi.Orders.Db;
using ECommerceApi.Orders.Interfaces;
using ECommerceApi.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Orders.Providers
{
    public class OrderProviders : IOrderProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderProviders> logger;
        private readonly IMapper mapper;

        public OrderProviders(OrderDbContext dbContext, ILogger<OrderProviders> logger,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.orders.Any())
            {
                dbContext.orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 101,
                    OrderDate = DateTime.Now,
                    Total = 50,
                    Items = new List<Db.OrderItem>() {
                        new Db.OrderItem() { OrderID =1,ProductID=1,Quantity=10,UnitPrice=10},
                        new Db.OrderItem() { OrderID =1,ProductID=2,Quantity=10,UnitPrice=1000},
                        new Db.OrderItem() { OrderID =2,ProductID=3,Quantity=10,UnitPrice=100},
                        new Db.OrderItem() { OrderID =3,ProductID=4,Quantity=1,UnitPrice=10},
                    }
                });
                dbContext.orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 102,
                    OrderDate = DateTime.Now.Date.AddDays(-1),
                    Total = 50,
                    Items = new List<Db.OrderItem>()
                    {
                         new Db.OrderItem() { OrderID =1,ProductID=1,Quantity=10,UnitPrice=10},
                        new Db.OrderItem() { OrderID =1,ProductID=2,Quantity=10,UnitPrice=1000},
                        new Db.OrderItem() { OrderID =2,ProductID=3,Quantity=10,UnitPrice=100},
                    }
                });
                dbContext.orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 103,
                    OrderDate = DateTime.Now.Date.AddDays(-2),
                    Total = 50,
                    Items = new List<Db.OrderItem>()
                    {
                         new Db.OrderItem() { OrderID =1,ProductID=1,Quantity=10,UnitPrice=10},
                        new Db.OrderItem() { OrderID =1,ProductID=2,Quantity=10,UnitPrice=1000},
                        new Db.OrderItem() { OrderID =2,ProductID=3,Quantity=10,UnitPrice=100},
                    }
                });
                dbContext.orders.Add(new Db.Order()
                {
                    Id = 4,
                    CustomerId = 104,
                    OrderDate = DateTime.Now.Date.AddDays(-3),
                    Total = 50,
                    Items = new List<Db.OrderItem>()
                    {
                         new Db.OrderItem() { OrderID =1,ProductID=1,Quantity=10,UnitPrice=10},
                        new Db.OrderItem() { OrderID =1,ProductID=2,Quantity=10,UnitPrice=1000},

                    }
                });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrderAsync()
        {
            try
            {
                var Orders = await dbContext.orders.ToListAsync();
                if (Orders != null && Orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(Orders);
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

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrderAsync(int Id)
        {
            try
            {
                var Orders = await dbContext.orders.
                    Where(P => P.Id == Id)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (Orders != null)
                {
                    var result = mapper.Map<IEnumerable<Db.Order>,IEnumerable<Models.Order>>(Orders);
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
