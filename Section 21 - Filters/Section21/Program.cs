using Section21.Filters.ActionFilters;
using Section21.Filters.AuthorizationFilter;
using Section21.Filters.FilterFactory;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region FilterArgumentActionFilter

// Creates a singleton Object which builds and registers the filter once
builder.Services.AddSingleton(provider =>
{
    var logger = provider.GetRequiredService<ILogger<FilterArgumentActionFilter>>();
return new FilterArgumentActionFilter(logger, "x-Filter-Level", "Global");
});

#endregion

#region IOrderedFilterAsync

builder.Services.AddSingleton(provider =>
{
    var logger = provider.GetRequiredService<ILogger<OrderFilterAsyncActionFilter>>();
    return new OrderFilterAsyncActionFilter(logger, "x-Filter-Level", "Global", 2);
});

#endregion

builder.Services.AddControllersWithViews(options =>
{
    #region Option 1) Drawback - we cannot pass arguments
    //options.Filters.Add<FilterArgumentActionFilter>();
    #endregion

    #region Option 2) - Creates a new temporary DI scope & Can cause duplicate singletons or premature instantiation

    //var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<FilterArgumentActionFilter>>();

    //options.Filters.Add(new FilterArgumentActionFilter(logger, "x-Filter-Level", "Global"));

    #endregion

    // Injecting Filter Services to Filters

    options.Filters.AddService<FilterArgumentActionFilter>();

    options.Filters.AddService<OrderFilterAsyncActionFilter>(2);

});

builder.Services.AddTransient<HomeAuthorizationFilter>();

builder.Services.AddTransient<HomeArgsFilter>();

builder.Host.UseSerilog((HostBuilderContext context,IServiceProvider services,LoggerConfiguration logConfig) =>
{
    logConfig.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
});

var app = builder.Build();

app.UseSerilogRequestLogging();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
