using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Posts.Commands;

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
    }
}
