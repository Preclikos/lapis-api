using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisLocationRepository
    {
        public Task<LapisLocation> GetLastByLapisId(int id, CancellationToken cancellationToken);
    }
}
