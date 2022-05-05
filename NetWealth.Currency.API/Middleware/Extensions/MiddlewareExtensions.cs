using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;

namespace QUBE.Web.API.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomIpRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRateLimitMiddleware>();
        }
    }
}
