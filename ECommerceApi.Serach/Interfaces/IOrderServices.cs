using ECommerceApi.Serach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Interfaces
{
    public interface IOrderServices
    {
       Task<(bool IsSuccess, IEnumerable<Order> orders ,string ErrorMessage)> GetOrdersAsync(int CustomerID);
    }
}
