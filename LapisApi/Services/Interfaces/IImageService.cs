using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services
{
    public interface IImageService
    {
        public Task<Image> GetLapisById(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<Image>> GetLapisById(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
