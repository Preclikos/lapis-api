using System.Collections.Generic;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class FeedService : IFeedService
    {
        public IEnumerable<FeedItem> GetFeedItems()
        {
            throw new System.NotImplementedException();
        }
    }
}
