using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IActivityRepository
    {
        public Task<IEnumerable<Activity>> GetLastActivities(int country, int offset, CancellationToken cancellationToken);
    }
}
