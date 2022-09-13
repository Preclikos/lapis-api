using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ISearchService searchService;
        public ActivityController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("Feed")]
        public IEnumerable<FeedItem> Feed(int country, int offset)
        {
            return new List<FeedItem>();
        }

    }
}
