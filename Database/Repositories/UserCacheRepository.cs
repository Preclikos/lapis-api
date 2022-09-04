using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;

namespace WebApi.Database.Repositories
{
    public class UserCacheRepository : IUserCacheRepository
    {
        private const int CacheTimeSpan = 1;
        private readonly IMemoryCache memoryCache;
        IUserRepository userRepository;

        public UserCacheRepository(IMemoryCache memoryCache, IUserRepository userRepository)
        {
            this.memoryCache = memoryCache;
            this.userRepository = userRepository;
        }

        public User OnGetCacheGetOrCreate(string key, string name)
        {
            var cacheKey = nameof(UserCacheRepository) + "_";
            return memoryCache.GetOrCreate<User>(
                cacheKey + key,
                cacheEntry => 
                { 
                    cacheEntry.SlidingExpiration = TimeSpan.FromHours(CacheTimeSpan);
                    return GetOrCreateDbUser(key, name); 
                });
        }

        private User GetOrCreateDbUser(string key, string name)
        {
            var user = userRepository.GetBySub(key);
            if(user == null)
            {
                user = new User() { Sub = key, Name = name };
                userRepository.Add(user);
            }
            else
            {
                userRepository.UpdateNameBySub(key, name);
            }

            return user;
        }

        public async Task OnGetCacheGetOrCreateAsync(string key)
        {
            var cacheKey = nameof(UserCacheRepository) + "_";
            var cachedValue = await memoryCache.GetOrCreateAsync(
                 cacheKey + key,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromHours(CacheTimeSpan);
                    return Task.FromResult(DateTime.Now);
                });

            // ...
        }

    }
}
