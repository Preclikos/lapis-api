using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApi.Attributes;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Responses.Models;
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

        [HttpGet("Code/{code}")]
        [ProxyDisableBuffer]
        public IAsyncEnumerable<SearchItem> Code(string code, CancellationToken cancellationToken)
        {
            var codeText = HttpUtility.UrlDecode(code);
            return searchService.GetLapisesByCode(codeText, cancellationToken);
        }

    }
}
