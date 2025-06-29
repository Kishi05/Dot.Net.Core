using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

#region To Check the headers and send values as part of Header and Response Object
//app.Run(async(HttpContext httpContext) =>
//{
//    httpContext.Response.Headers["Test"] = "New";
//    httpContext.Response.StatusCode = 401;
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsync("<div><h1>First Run Hit - Middleware</h1></div>");
//});
#endregion

#region Read URL and Method value from httpContext
//app.Run( async (HttpContext httpContext) =>
//{
//    var path = httpContext.Request.Path; // URL
//    var method = httpContext.Request.Method; // HTTP Method
//    await httpContext.Response.WriteAsync($"First Run Hit - {path} Path - Middleware");
//});
#endregion

#region Read Query String from HttpContext.Request if its a Get request ( Nothing specific for using GET just for example)
//app.Run( async (HttpContext httpContext) => 
//{
//    var path = httpContext.Request.Path;
//    var method = httpContext.Request.Method;
//    if (method == "GET")
//    {
//        var queryString = httpContext.Request.Query.AsQueryable().ToDictionary();
//        foreach( var kvp in queryString)
//        {
//            var key = kvp.Key;
//            var value = kvp.Value;
//            await httpContext.Response.WriteAsync($"Key - {key}, Value - {value}\n");
//        }
//    }
//    await httpContext.Response.WriteAsync($"First Run Hit - Middleware");
//});
#endregion

#region Similarlly you can also take values from Header
//app.Run(async (HttpContext httpContext) =>
//{
//    var path = httpContext.Request.Path;
//    var method = httpContext.Request.Method;
//    var headers = httpContext.Request.Headers.ToDictionary();
//    string text = "<table> <tr> <td>Key</td> <td>Value</td> </tr>";
//    foreach (var kvp in headers)
//    {
//        var key = kvp.Key;
//        var value = kvp.Value;
//        text += ($"<tr><td>{key} </td><td> {value}</td><tr>");
//    }
//    text += ("</table>");
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsync(text);
//});
#endregion

#region Read Auth Key values from Header
//app.Run(async (HttpContext httpContext) =>
//{
    
//    var headers = httpContext.Request.Headers;
//    string result = headers.ContainsKey("Authorization") ? headers["Authorization"].ToString():"Key Not Found";

//    //Need Stream Reader to read body as its a stream type
//    StreamReader reader = new StreamReader(httpContext.Request.Body);
//    var body = await reader.ReadToEndAsync();

//    string name = string.Empty;
//    if(!string.IsNullOrEmpty(body))
//    {
//        Dictionary<string,StringValues> query = QueryHelpers.ParseQuery(body);
//        name = query.ContainsKey("name") ? query["name"].ToString() : "Key Not Found";
//    }

//    await httpContext.Response.WriteAsync($"{result} -- {body} -- {name}");
//});
#endregion


app.Run();
