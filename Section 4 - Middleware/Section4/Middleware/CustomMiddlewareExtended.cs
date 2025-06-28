namespace Section4.Middleware
{
    public class CustomMiddlewareExtended
    {
        private readonly RequestDelegate _next;
        public CustomMiddlewareExtended(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) 
        {
            await context.Response.WriteAsync("Custom Middleware Extend using Middleware Class 1-1\n");
            await _next(context);
        }
    }

    public static class CustomMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomMiddlewareExtentions(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomMiddlewareExtended>();
            return app;
        }
    }
}
