using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IUserRepository
    {
        public int Add(User entity);

        public Task<User> GetById(int id, CancellationToken cancellationToken);

        public Task<UserProfile> GeProfiletById(int id, CancellationToken cancellationToken);

        public User GetBySub(string sub);

        public Task<User> GetBySub(string sub, CancellationToken cancellationToken);

        public int UpdateNameBySub(string sub, string name);
    }
}
