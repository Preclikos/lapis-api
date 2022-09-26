using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;

namespace WebApi.Services
{
    public interface IImageService
    {
        public Task<Image> GetLapisImageById(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<Image>> GetById(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
