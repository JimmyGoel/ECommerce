using ECommerceApi.Serach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync();
    }
}
