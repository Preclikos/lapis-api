using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IUserCacheRepository
    {
        public User OnGetCacheGetOrCreate(string key, string name);
    }
}
