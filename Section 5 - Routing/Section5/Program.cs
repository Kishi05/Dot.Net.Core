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

#region Routing Parameter

//app.UseRouting();

//app.UseEndpoints(endpoints => {

//    //Parameter and = should not have space, if so it works as vague -> shows value in object but when we try to get it or view in quick watch it shows null
//    //Advise to keep param and = without space

//    endpoints.MapGet("{id}/{name=Kishi}", async (context) =>

//    {
//        var routeValues = context.Request.RouteValues;
//        await context.Response.WriteAsync($"Hello World {routeValues["id"]} - {routeValues["name"]}");
//    });
//});

#endregion

#region Route Constraints -  Parameter With specific Type

//app.UseRouting();

//app.UseEndpoints(endpoints => {
//    endpoints.Map("/{id:int}",async (context) =>
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter INT - {routeValues["id"]}");
//    });

//    endpoints.Map("/{id:string}", async (context) => //ideally type - string doesnt need declaration and also should not declare data type throws error -
//                                                     //InvalidOperationException: The constraint reference 'string' could not be resolved to a type. Register the constraint type with 'Microsoft.AspNetCore.Routing.RouteOptions.ConstraintMap'.
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter String - {routeValues["id"]}");
//    });

//    endpoints.Map("/{id:guid}", async (context) =>
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter GUID - {routeValues["id"]}");
//    });
//});

#endregion

#region Optional Parameter

//app.UseRouting();

//app.UseEndpoints(endpoints => {
//    endpoints.Map("/op1/{id = null}", async (context) =>
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter explicit default null declaration - {routeValues["id"]}");
//    });

//    endpoints.Map("/op2/{id?}", async (context) => 
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter using ? - {routeValues["id"]}");
//    });

//    endpoints.Map("/op3/{id:guid?}", async (context) =>
//    {
//        RouteValueDictionary? routeValues = context.Request.RouteValues;

//        await context.Response.WriteAsync($"Optional Parameter with data type GUID and ? - {routeValues["id"]}");
//    });
//});

#endregion

#region Route Constraints -  Parameter With Pre-defined Func Methods

//app.UseRouting();

//// Interesting Fact : here Id is declared as int so if the value of id parameter exceeds int range api fails as 404
//// Int range : -2,147,483,648 to 2,147,483,647
//// any value higher than this change type with max bits.

//app.UseEndpoints(endpoints => {

//    //endpoints.Map("/param/{id:int:max(5)}", async (contex) =>
//    //{
//    //    RouteValueDictionary routeValue = contex.Request.RouteValues;
//    //    await contex.Response.WriteAsync("Route Value INT : " + routeValue["id"]);
//    //});

//    endpoints.Map("/param/{id:float:max(5)}", async (contex) =>
//    {
//        RouteValueDictionary routeValue = contex.Request.RouteValues;
//        await contex.Response.WriteAsync("Route Value Float : " + routeValue["id"]);
//    });

//});

#endregion

app.Run();
