using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Products.Db
{
    public class ProductDbContext :DbContext 
    {
        public DbSet<Product> Products{ get; set; }
        public ProductDbContext( DbContextOptions Options):base(Options)
        {

        }
    }
}
