
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface ILapisService
    {
        public Task<Lapis> GetById(int id, CancellationToken cancellationToken);
        public Task<LapisActivity> GetLapisActivities(int id, int offset, CancellationToken cancellationToken);
    }
}
