using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IFeedService feedService;
        public ActivityController(IFeedService  feedService)
        {
            this.feedService = feedService;
        }

        [HttpGet("Feed")]
        public async Task<Feed> Feed(int country, int offset, CancellationToken cancellationToken)
        {
            return await feedService.GetFeedItems(0, country, cancellationToken);
        }

    }
}
