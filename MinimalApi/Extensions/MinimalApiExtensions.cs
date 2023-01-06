using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Posts.Commands;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder) {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(connectionString));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(typeof(CreatePost));
        }
        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endPointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndPointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndPointDefinition>();
            foreach (var endPointDefinition in endPointDefinitions)
            {
                endPointDefinition.RegisterEndPoint(app);

            }

        }
    }
}
