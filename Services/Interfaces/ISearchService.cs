using System.Collections.Generic;
using System.Threading;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface ISearchService
    {
        public IAsyncEnumerable<SearchItem> GetLapisesByCode(string code, CancellationToken cancellationToken);
    }
}
