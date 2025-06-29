using Section7.CustomIModelBinderProvider;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers(); // Default

builder.Services.AddControllers(options => {
    options.ModelBinderProviders.Insert(0, new RegisterModelBindProvider()); // Register Custom Model Binder Provider to be consumed inside Project
});

//Include XML as Formatter
//builder.Services.AddControllers().AddXmlSerializerFormatters(); // Comment it when not using xml

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
