using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class MiddlewareExtensions
    {

        public static IApplicationBuilder UseFactoryActivatedMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<FactoryActivatedMiddleware>();
    }
}
