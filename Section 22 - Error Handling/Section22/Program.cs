using Section22.CustomExceptions;
using Section22.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    //Custom Middleware
    app.UseExceptionHandlingMiddleware();
}

//Default Handler
app.UseExceptionHandler("/Error");

app.MapGet("/", () =>
{
    //Custom Handler
    throw new CustomExceptionHandler("Custom Error");
});

app.Run();
