using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Differnt ways of using Route using Middleware

//app.UseRouting()

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async (context) =>
//    {
//        await context.Response.WriteAsync("Endpoint MapGet 1-0");
//    });

//    endpoints.MapPost("/", async (context) =>
//    {
//        await context.Response.WriteAsync("Endpoint MapPost 1-0");
//    });

//    endpoints.Map("/map1", async (context) =>
//    {
//        await context.Response.WriteAsync("Endpoint Map1 1-0");
//    });

//    endpoints.Map("/map2", async (context) =>
//    {
//        await context.Response.WriteAsync("Endpoint Map2 1-0");
//    });

//});

#endregion

#region GetEndpoint using Routing

//GetEndpoint will return object such as 'Path' ('/map) and Method ('GET')

////Since GetEndpoint is called before routing it always returns NULL
//app.Use(async (httpContext, next) => {
//    Endpoint? endpoint = httpContext.GetEndpoint();
//    await next(httpContext);
//});

////Enable Routing in project - Only after initializing context object is obtained
//app.UseRouting();

//// After routing if endpoint is mapped (intentionally even for base url '/') -> GetEndpoint will return value or defaults null.

//app.Use(async (httpContext, next) => {
//    Endpoint? endpoint = httpContext.GetEndpoint();
//    await next(httpContext);
//});

////Endpoint or Route should be defined inorder for the GetEndpoint method to return a value or defaults null.
////Placing Below endpoint above use => Route hits and pushes response to browser and since no response delegate in use endpoint it will not move to next middleware
//app.UseEndpoints(endpoints =>
//{
//    endpoints.Map("/", async (context) =>
//    {
//        await context.Response.WriteAsync("Endpoint Map1 1-0");
//    });
//});

#endregion

app.Run();
