using ECommerceApi.Serach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Interfaces
{
    public interface ISerachServices
    {
        Task<(bool IsSucess, dynamic SearchResult)> GetSearchAsync(int CustomerID);
    }
}
