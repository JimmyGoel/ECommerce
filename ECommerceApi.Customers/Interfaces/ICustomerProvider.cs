using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomerAsync();
        Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int Id);
    }
}
