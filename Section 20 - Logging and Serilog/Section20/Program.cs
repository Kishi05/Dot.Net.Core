using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Services.Users;
using Services.Users.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsers, Users>();

//Configure the logging
builder.Logging.ClearProviders().AddConsole().AddDebug();

#region Bulder Log Config 

//other way to configure Logging
//builder.Host.ConfigureLogging(loggingProvider =>
//{
//    loggingProvider.ClearProviders();
//    loggingProvider.AddConsole();
//});

#endregion

//Configure Http Logging
builder.Services.AddHttpLogging(_ => {

    //This will log all the properties. Check the Enum for types of properties to log
    // You can combine multiple seperated with |
    // eg : _.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.None;

    _.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

builder.Services.AddControllersWithViews();

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

// Log Http Calls
app.UseHttpLogging();

#region Logging - Manual

//app.Logger.LogDebug("Debug - Message");
//app.Logger.LogInformation("Information - Message");
//app.Logger.LogWarning("Warning - Message");
//app.Logger.LogError("Error - Message");
//app.Logger.LogCritical("Critical - Message");

#endregion

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );



app.Run();
