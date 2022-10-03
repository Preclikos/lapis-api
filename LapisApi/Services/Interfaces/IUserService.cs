using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetById(int id, CancellationToken cancellationToken);

        public Task<User> GetBySub(string sub, CancellationToken cancellationToken);
    }
}
