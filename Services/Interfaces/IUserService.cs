using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserById(int id, CancellationToken cancellationToken);

        public Task<User> GetUserBySub(string sub, CancellationToken cancellationToken);
    }
}
