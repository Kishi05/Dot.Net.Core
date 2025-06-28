var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async (context) =>
    {
        await context.Response.WriteAsync("Endpoint MapGet 1-0");
    });
    endpoints.MapPost("/", async (context) =>
    {
        await context.Response.WriteAsync("Endpoint MapGet 1-0");
    });
});

app.Run();
