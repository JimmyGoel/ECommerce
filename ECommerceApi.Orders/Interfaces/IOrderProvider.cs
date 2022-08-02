using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, String ErrorMessage)> GetOrderAsync();
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, String ErrorMessage)> GetOrderAsync(int CustomerID);
    }
}
