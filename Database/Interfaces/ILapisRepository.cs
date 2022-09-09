using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisRepository
    {
        public int Add(Lapis entity);

        public IAsyncEnumerable<int> GetIdByCode(string country, string region, string user, string lapis, CancellationToken cancellationToken);

        public Task<Lapis> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
