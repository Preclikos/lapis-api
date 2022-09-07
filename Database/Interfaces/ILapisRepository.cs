using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface ILapisRepository
    {
        public int Add(Lapis entity);

        public IAsyncEnumerable<Lapis> SearchByCode(string code);
    }
}
