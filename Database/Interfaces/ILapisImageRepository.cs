using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisImageRepository
    {
        public Task<LapisImage> GetById(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<LapisImage>> GetAllByLapisId(int lapisId, CancellationToken cancellationToken);

        public Task<IEnumerable<LapisImage>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken);

    }
}
