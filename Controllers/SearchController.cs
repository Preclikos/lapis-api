using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Attributes;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("Code")]
        [ProxyDisableBuffer]
        public IAsyncEnumerable<Lapis> Code(string code, CancellationToken cancellationToken)
        {
            return searchService.GetLapisesByCode(code, cancellationToken);
        }

    }
}
