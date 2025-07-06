using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Section27.BL;
using Section27.BL.Interface;
using Section27.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options =>
{
    options.Filters.Add(new ProducesAttribute("application/json")); // Request Body
    options.Filters.Add(new ConsumesAttribute("application/json")); // Respose Body
});

builder.Services.AddApiVersioning(config => {
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); // Part of URI

    //config.ApiVersionReader = new QueryStringApiVersionReader("version"); // Part of Query String ?'version'(<- changed)=1.0  - Default: api-version=1.0

    //config.ApiVersionReader = new HeaderApiVersionReader(); // Part of Headers // Default: api-version=1.0 - can override similar as above

    config.DefaultApiVersion = new ApiVersion(1,0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Exposing Endpoint
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));  //Display Comments in Swagger

    //Create Swagger document seperately for each version created
    options.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title="User API",
            Version = "1.0",
        });

    options.SwaggerDoc("v2",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "User API",
            Version = "2.0",
        });

}); // Add Swager

builder.Services.AddVersionedApiExplorer(options =>{
    options.GroupNameFormat = "'v'VVV";  //v1
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddScoped<IUserCoordinator, UserCoordinator>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
    });
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
