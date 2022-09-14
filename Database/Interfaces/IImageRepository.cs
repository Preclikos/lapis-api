using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IImageRepository
    {
        public Task<Image> GetById(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<Image>> GetAllByLapisId(int lapisId, CancellationToken cancellationToken);

        public Task<IEnumerable<Image>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken);

    }
}
