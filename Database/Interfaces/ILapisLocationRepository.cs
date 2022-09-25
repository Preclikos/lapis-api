using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisLocationRepository
    {
        public Task<LapisLocation> GetById(int id, CancellationToken cancellationToken);

        public Task<LapisLocation> GetLastByLapisId(int id, CancellationToken cancellationToken);
    }
}
