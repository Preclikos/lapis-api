using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserById(int id, CancellationToken cancellationToken);
    }
}
