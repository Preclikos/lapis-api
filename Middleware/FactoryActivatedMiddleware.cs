using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;

namespace WebApi.Middleware
{
    public class FactoryActivatedMiddleware : IMiddleware
    {
        private readonly IUserCacheRepository userCacheRepository;
        private const string subType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private const string nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

        public FactoryActivatedMiddleware(IUserCacheRepository userCacheRepository)
        {
            this.userCacheRepository = userCacheRepository;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var claims = context.User.Claims;

            if (claims.Any(cl => cl.Type == subType) && claims.Any(cl => cl.Type == nameType))
            {

                userCacheRepository.OnGetCacheGetOrCreate(claims.Single(s => s.Type == subType).Value, claims.Single(s => s.Type == nameType).Value);
            }

            await next(context);
        }
    }
}
