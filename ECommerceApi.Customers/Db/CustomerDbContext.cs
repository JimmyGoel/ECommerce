using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Customers.Db
{
    public class CustomerDbContext:DbContext
    {
        public DbSet<Db.Customer> customers { get; set; }
        public CustomerDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
