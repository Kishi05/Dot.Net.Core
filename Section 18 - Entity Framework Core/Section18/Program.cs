using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NetCoreAppDBContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocal"))
        //This is specifically to suppress the Warning occuring as we have dynamic values in our Seed Has Data.
        //Ideal fix is to make those dynamic values static
        //Just Added this to check this behaviour. Since in our case its Created ON and Modified ON - which to my perspective need to dynamically change during run
        .ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
    });

var app = builder.Build();

app.Run();
