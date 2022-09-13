using System.Collections;
using System.Collections.Generic;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface IFeedService
    {
        public IEnumerable<FeedItem> GetFeedItems();
    }
}
