using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface IFeedService
    {
        public Task<IEnumerable<FeedItem>> GetFeedItems(int country, int offset, CancellationToken cancellationToken);
    }
}
