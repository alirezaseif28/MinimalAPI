using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

// Add services to the container.

var app = builder.Build();

app.Use(async (ctx,next) => {
	try
	{
		await next();
	}
	catch (Exception)
	{
		ctx.Response.StatusCode = 500;
		await ctx.Response.WriteAsync("An error ocurred");
	}
});


app.RegisterEndpointDefinitions();

app.Run();
