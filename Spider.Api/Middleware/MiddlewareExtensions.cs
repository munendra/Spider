using Microsoft.AspNetCore.Builder;
using Spider.Api.Middleware.Exception;

namespace Spider.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}