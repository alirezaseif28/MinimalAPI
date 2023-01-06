using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

// Add services to the container.

var app = builder.Build();

app.RegisterEndpointDefinitions();

app.Run();
