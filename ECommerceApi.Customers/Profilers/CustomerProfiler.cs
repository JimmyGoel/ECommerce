using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Customers.Profilers
{
    public class CustomerProfiler: AutoMapper.Profile
    {
        public CustomerProfiler()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
