var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();



app.UseStaticFiles();
app.MapControllers();
app.MapDefaultControllerRoute();

//Usage of Environment in Launch Settings
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();

/*
 * Apart of 
 *      1. Launch Screen, 
 *      2. Controller
 *      3. Tag Helper
 *  We can also send values from machine to Application process - while build/run application using terminal
 *  
 *      dotnet run --no-launch-profile <- This will ignore exisitng launch settings and thus points as Production
 *      
 *                                                  (or)
 *                                                  
 *      We can set Env value in Terminal (Local memory) and App to use it
 *      
 *      $ENV:ASPNETCORE_ENVIRONMENT="Staging" <- No Spaces
 *      dotnet run --no-launch-profile <- Now it will point to Staging
 */