using Section14.Services;
using Section14.Services.Interface;
using Section14.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Config"));
builder.Services.AddSingleton<IConfig, Config>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.UseEndpoints(endpoints => {
    endpoints.Map("/", async (context) =>
    {
        //Direct
        var configValue = app.Configuration["Config:Key"];
        await context.Response.WriteAsync("Direct : "+configValue+"\n");

        //Using Get Value
        var configValue1 = app.Configuration.GetValue<string>("Config:Key");
        await context.Response.WriteAsync("Get Value : " + configValue1 + "\n");

        //Get Value with Default
        var configValuedefault = app.Configuration.GetValue<string>("Config:CS","Value from Program cs since CS is not part of app settings");
        await context.Response.WriteAsync("Get Value : " + configValuedefault + "\n");
    });
});

app.Run();
