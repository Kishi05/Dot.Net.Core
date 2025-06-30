using Services;
using Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//One Way of injecting Dependency
//builder.Services.Add(new ServiceDescriptor(
//    typeof(IProgramLanguageService),
//    typeof(ProgramLanguageService),
//    ServiceLifetime.Transient
//    ));

//UnComment any one type below to check the scenario

//new instance each and every hit
builder.Services.AddTransient<IProgramLanguageService, ProgramLanguageService>();

////new instance per transaction -> Network call
//builder.Services.AddScoped<IProgramLanguageService, ProgramLanguageService>();

////new instance only on server refresh/restart
//builder.Services.AddSingleton<IProgramLanguageService, ProgramLanguageService>();

var app = builder.Build();

app.MapControllers();

app.Run();
