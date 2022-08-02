using ECommerceApi.Serach.Interfaces;
using ECommerceApi.Serach.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Serach.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        private readonly ISerachServices searchService;

        public SearchController(ISerachServices searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm team)
        {
            var result = await searchService.GetSearchAsync(team.CustomerID);
            if(result.IsSucess)
            {
                return Ok(result.SearchResult);
            }
            return NotFound();
        }
    }
}
