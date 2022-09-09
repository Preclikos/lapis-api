using System.Collections.Generic;
using System.Threading;
using WebApi.Database.Models;

namespace WebApi.Services.Interfaces
{
    public interface ISearchService
    {
        public IAsyncEnumerable<Lapis> GetLapisesByCode(string code, CancellationToken cancellationToken);
    }
}
