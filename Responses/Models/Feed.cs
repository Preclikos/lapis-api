using System.Collections.Generic;

namespace WebApi.Responses.Models
{
    public class Feed
    {
        public Feed(int responseLimit)
        {
            ResponseLimit = responseLimit;
            FeedItems = new List<FeedItem>();
        }

        public int ResponseLimit { get; set; }
        public IEnumerable<FeedItem> FeedItems { get; set; }
    }
}
