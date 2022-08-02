using ECommerceApi.Serach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Interfaces
{
    public interface IProductServices
    {
        Task<(bool ISSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsyn();
    }
}
