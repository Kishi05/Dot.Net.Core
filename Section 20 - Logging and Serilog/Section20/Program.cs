/*  ─────────────────────────────────────────────────────────────────────────────
    Program.cs – Section Map & Logging Guide
    ─────────────────────────────────────────────────────────────────────────────
    ▸ The Program file is growing as new sections are added.  To keep it readable,
      code blocks are grouped under #region directives that mirror the course modules
      completed so far.

    ▸ LOGGING
        #region Logging
            #region Built‑in Logging
              • Console / Debug providers
              • HTTP request logging (HttpLoggingMiddleware)
            #endregion

            #region Serilog
              • Structured logging pipeline
              • File and console sinks
            #endregion
        #endregion

      –  HttpLoggingMiddleware captures HTTP‑protocol details (method, path, headers,
         status code, body) and routes them through the configured logging providers.

    --------------------------------------------------------------------------- */


using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Services.Users;
using Services.Users.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsers, Users>();

#region Section 20 - Logging

#region Type 1 -> Built IN - Bulder Log Config 

// UnComment either (1) or (2) to enable Built IN Logger Functionality

//                             Logging Configuration
// ----------------------------------------------------------------------- //

//builder.Logging.ClearProviders().AddConsole().AddDebug(); // UnComment (1)

// other way to configure Logging

//builder.Host.ConfigureLogging(loggingProvider => //UnComment(2)
//{
//    loggingProvider.ClearProviders();
//    loggingProvider.AddConsole();
//});



//                                  Http Logging
// ----------------------------------------------------------------------- //

// NOTE: Adjusted from course code (2022) to .NET 8 standard
// - HttpLoggingFields should be explicitly set - previously it was called internammy when UseHttpLogging() method is called.

builder.Services.AddHttpLogging(_ => {

    // You can combine multiple properties seperated by '|'

    // _.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestHeaders;

    // Below line fetch all properties except ResponseBody which is denoted by '~' symbol. Get All and ignore Request Body

    _.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All & ~Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;
});

#endregion

#region Type 2 -> SeriLog

//                              Serilog Configuration
// ----------------------------------------------------------------------- //

// If LoggerConfiguration only needed
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration)   //Reads static settings from appsettings.json, appsettings.Development.json, environment variables, etc.
    .ReadFrom.Services(services);   //Pulls runtime‑registered services (e.g., IHttpContextAccessor, custom enrichers, or DI‑configured sinks) into the Serilog pipeline.
});

#endregion

#endregion

#region Previous Section - continuation 

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

#endregion

app.UseSerilogRequestLogging();

// Section 20 -> Log Http Calls
app.UseHttpLogging();

#region Section 20 - Logging - Manual

//app.Logger.LogDebug("Debug - Message");
//app.Logger.LogInformation("Information - Message");
//app.Logger.LogWarning("Warning - Message");
//app.Logger.LogError("Error - Message");
//app.Logger.LogCritical("Critical - Message");

#endregion

#region Previous Section - continuation 

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

#endregion

app.Run();
