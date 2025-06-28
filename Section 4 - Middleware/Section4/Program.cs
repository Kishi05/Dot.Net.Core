using Section4.Middleware;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddTransient<CustomExtendMiddleware>();

var app = builder.Build();

#region Middleware Chain

//app.Use(async (HttpContext httpContext, RequestDelegate next) =>
//{
//    await httpContext.Response.WriteAsync("MiddleWare 1-1\n");
//    await next(httpContext);
//    await httpContext.Response.WriteAsync("MiddleWare 1-2\n");
//});

//app.Use(async (HttpContext httpContext, RequestDelegate next) =>
//{
//    await httpContext.Response.WriteAsync("MiddleWare 2-1\n");
//    await next(httpContext);
//    await httpContext.Response.WriteAsync("MiddleWare 2-2\n");
//});

//app.Run(async (HttpContext httpContext) =>
//{
//    await httpContext.Response.WriteAsync("Middleware 3-0\n");
//});

#endregion

#region CustomMiddleware - Initial class with only invoke method from IMiddleware

//app.UseMiddleware<CustomMiddleware>();

#endregion

#region CustomMiddleware - Extenstion class Manual with invoke and Generic Extension method from IMiddleware

//app.UseCustomExtendMiddleware();

#endregion

#region CustomMiddleware - Middleware Extend

//app.UseCustomMiddlewareExtentions();

#endregion

#region USE WHEN

//// This will invoke only if Query String as ID parameter in it
//app.UseWhen(
//    context => context.Request.Query.ContainsKey("ID"),
//    app =>
//    {
//        app.Use(async (HttpContext httpContext, RequestDelegate next) =>
//        {
//            await httpContext.Response.WriteAsync("Use WHEN 1-1\n");
//            await next(httpContext);
//        });
//    }
//    );

//// Since theres no middleware to handle default URL - This will get called for all URL
//app.Use(async (HttpContext httpContext, RequestDelegate next) =>
//{
//    await httpContext.Response.WriteAsync("Default Route Handler 1-1\n");
//    await next(httpContext);
//});

#endregion

#region Different Ways of having MapGet which will make sense with future Sections

app.MapGet("/", async (HttpContext httpContext) => await httpContext.Response.WriteAsync("Welcome"));

app.MapGet("/hello", () => "Hello World");

#endregion

app.Run();