using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Add services to the container.

var app = builder.Build();


app.Run();
