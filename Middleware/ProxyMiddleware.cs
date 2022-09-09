using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Threading.Tasks;
using WebApi.Attributes;

namespace WebApi.Middleware
{
    public class ProxyMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<ProxyDisableBufferAttribute>();
            if (attribute != null)
            {
                context.Response.Headers.Add("X-Accel-Buffering", "no");

            }

            await next(context);
        }
    }
}
