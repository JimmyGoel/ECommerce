using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Orders.Db
{
    public class OrderDbContext:DbContext
    {
        public DbSet<Db.Order> orders { get; set; }
        public DbSet<Db.OrderItem> orderItems { get; set; }
        public OrderDbContext(DbContextOptions options) :base(options)
        {

        }
    }
}
