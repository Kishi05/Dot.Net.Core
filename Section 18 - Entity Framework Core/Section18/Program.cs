using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Services.Users.Interface;
using Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsers, Users>();

builder.Services.AddDbContext<NetCoreAppDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocal"))
        //This is specifically to suppress the Warning occuring as we have dynamic values in our Seed Has Data.
        //Ideal fix is to make those dynamic values static
        //Just Added this to check this behaviour. Since in our case its Created ON and Modified ON - which to my perspective need to dynamically change during run
        .ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
    });

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
