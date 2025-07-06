using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Section25.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public UserAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            SignInManager<ApplicationUser>? _signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
            if (_signInManager.IsSignedIn(httpContext.User))
            {
                await _next(httpContext);
            }
            else
            {
                await httpContext.ChallengeAsync();
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserAuthMiddleware>();
        }
    }
}
