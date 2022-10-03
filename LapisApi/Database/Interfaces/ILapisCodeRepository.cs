using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisCodeRepository
    {
        public Task<LapisCode> GetByLapisId(int id, CancellationToken cancellationToken);

        public IAsyncEnumerable<LapisCode> GetIdAndCodeByCode(string country, string region, string user, string lapis, CancellationToken cancellationToken);
    }
}
