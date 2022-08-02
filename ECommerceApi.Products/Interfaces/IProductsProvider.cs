using ECommerceApi.Products.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSucess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSucess, Models.Product product, string ErrorMessage)> GetProductAsync(int Id);
        
    }
}
