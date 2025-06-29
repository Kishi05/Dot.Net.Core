var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Include XML as Formatter
builder.Services.AddControllers().AddXmlSerializerFormatters();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
