using Microsoft.AspNetCore.Hosting.Builder;
using System.Runtime.CompilerServices;

namespace Section4.Middleware
{
    public class CustomExtendMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Extend Middleware 1-1\n");
            await next(context);
        }
    }

    public static class CustomExtendMiddlewareClass
    {
        public static IApplicationBuilder UseCustomExtendMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CustomExtendMiddleware>();
            return applicationBuilder;
        }
    }
}
