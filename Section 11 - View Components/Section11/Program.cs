var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
