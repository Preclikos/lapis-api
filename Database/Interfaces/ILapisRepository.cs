using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisRepository
    {
        public int Add(Lapis entity);

        public IAsyncEnumerable<int> GetIdByCode(int country, int region, int user, int lapis, CancellationToken cancellationToken);

        public Task<Lapis> GetById(int id);
    }
}
