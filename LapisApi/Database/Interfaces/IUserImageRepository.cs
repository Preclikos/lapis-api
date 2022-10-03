using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IUserImageRepository
    {
        public Task<UserImage> GetById(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<UserImage>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken);

    }
}
