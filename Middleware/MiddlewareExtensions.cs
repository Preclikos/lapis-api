using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class MiddlewareExtensions
    {

        public static IApplicationBuilder UseUserNameMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<UserNameMiddleware>();

        public static IApplicationBuilder UseProxyMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<ProxyMiddleware>();
    }
}
