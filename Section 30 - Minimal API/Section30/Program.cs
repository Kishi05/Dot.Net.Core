using Microsoft.AspNetCore.Mvc;
using Section30;
using Section30.Endpoint_filter;
using Section30.RouteGroups;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Default line of code
//app.MapGet("/", () => "Hello World!");

#region Simple

//Same URL but differenciated based on HTTP TYPE
app.MapGet("/", async (context) =>
{
    await context.Response.WriteAsync("Welcome to Minimal API - GET");
});

// Use PostMan to run below API;s
//http://localhost:5105 <- With Different HTTP Types.
// You can find the url either in launchSettings.json or Kestral Console.
app.MapPost("/", async (context) =>
{
    await context.Response.WriteAsync("Welcome to Minimal API - POST");
});


app.MapPut("/", async (context) =>
{
    await context.Response.WriteAsync("Welcome to Minimal API - PUT");
});


app.MapDelete("/", async (context) =>
{
    await context.Response.WriteAsync("Welcome to Minimal API - DELETE");
});

#endregion

#region User Object related

//Users
List<User> userList = new List<User>() {
    new User()
    {
        Id = 1,
        Name = "Jack"
    },
    new User()
    {
        Id = 2,
        Name = "Sam"
    }
};

// User GET ALL
app.MapGet("/user",async (context) =>
{
    string result = string.Join("\n", userList.Select(x => x.ToString()));
    await context.Response.WriteAsync(result);
});

// User Create - Use PostMan
app.MapPost("/user", async (HttpContext context,[FromBody]User? user) =>
{
    if (user is null)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid User Details");
        return;
    }
    userList.Add(user);
    string result = string.Join("\n", userList.Select(x => x.ToString()));
    await context.Response.WriteAsync(result);
});

// Get User based on ID
app.MapGet("/user/{id:int}", async (HttpContext context, int id) =>
{
    User? user = userList.FirstOrDefault(x => x.Id == id);
    await context.Response.WriteAsync((user is not null)?user.ToString():"Invalid ID");
});

//With STJ Serializer
// User GET ALL
app.MapGet("/STJ/user", async (context) =>
{    
    await context.Response.WriteAsync(JsonSerializer.Serialize(userList));
});

app.MapGet("/STJ/user/{id:int}", async (HttpContext context, int id) =>
{
    User? user = userList.FirstOrDefault(x => x.Id == id);
    await context.Response.WriteAsync(JsonSerializer.Serialize(user));
});

// string is not a predefiened decorator we can use. 
// string is the defauly type and explicitly mentioning it will provide runtime errors.
// InvalidOperationException: The constraint reference 'string' could not be resolved to a type. 
// Register the constraint type with 'Microsoft.AspNetCore.Routing.RouteOptions.ConstraintMap' 

//app.MapGet("/STJ/user/{name:string}", async (HttpContext context, string name) =>
//{
//    User? user = userList.FirstOrDefault(x => x.Name == name);
//    await context.Response.WriteAsync(JsonSerializer.Serialize(user));
//});

#endregion

#region MapGroup

// Creating Group methods in Program.cs
RouteGroupBuilder? mapGroup = app.MapGroup("/usersG1"); // Route Attribute on Controller level.

mapGroup.MapGet("/",async (context) => //<- Methods inside controller
{
    await context.Response.WriteAsync("Inside Users Get");
});

// Calling Methods from seperate extention file
app.MapGroup("/usersG2").UserAPI();

#endregion

#region IResult

app.MapGet("/IResult/user/{id:int}", async (HttpContext context, int id) =>
{
    User? user = userList.FirstOrDefault(x => x.Id == id);
    if (user is null)
        return Results.BadRequest(new {message = "Invalid ID"}); // Specific Return Methods
    return Results.Ok(user);
});

#endregion

#region END Point

app.MapPost("/endpoint/users", async (HttpContext context, [FromBody] User? user) =>
{

    if (user is null)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid User Details");
        return;
    }
    userList.Add(user);
    string result = string.Join("\n", userList.Select(x => x.ToString()));
    await context.Response.WriteAsync(result);

}).AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
{
    var user = context.Arguments.OfType<User>().FirstOrDefault();
    if (user is null)
        return Results.BadRequest("User Object should not be NULL");

    ValidationContext? validate = new ValidationContext(user);
    List<ValidationResult> validationResults = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(user,validate,validationResults,true);

    if (!isValid)
    {
        return Results.BadRequest(validationResults.FirstOrDefault()?.ErrorMessage);
    }


    var result = await next(context);


    return result;
});

#endregion

#region IENDPoint

app.MapPost("/endpoint/multi/users", async (HttpContext context, [FromBody] User? user) =>
{
    //Hits Third

    if (user is null)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid User Details");
        return;
    }
    userList.Add(user);
    string result = string.Join("\n", userList.Select(x => x.ToString()));
    await context.Response.WriteAsync(result);

}).AddEndpointFilter<CustomEndPoint>()
.AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
{
    //Hits Second

    var user = context.Arguments.OfType<User>().FirstOrDefault();
    if (user is null)
        return Results.BadRequest("User Object should not be NULL");

    ValidationContext? validate = new ValidationContext(user);
    List<ValidationResult> validationResults = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(user, validate, validationResults, true);

    if (!isValid)
    {
        return Results.BadRequest(validationResults.FirstOrDefault()?.ErrorMessage);
    }


    var result = await next(context);

    //Hits Fourth

    return result;
});

#endregion

app.Run();
