using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddMediatR(typeof(CreatePost));

// Add services to the container.

var app = builder.Build();

#region GetPostById
app.MapGet("/api/posts/{id}", async (IMediator mediator, long id) =>
{
    var getPost = new GetPostById() { PostId = id };
    var post = await mediator.Send(getPost);
    return Results.Ok(post);
}).WithName("GetPostById");
#endregion

#region CreatePost
app.MapPost("/api/posts", async (IMediator mediator, Post post) =>
 {
     var createPost = new CreatePost() { PostContent = post.Content };
     var createdPost = await mediator.Send(createPost);
     return Results.CreatedAtRoute("GetPostById", new { createdPost.ID }, createdPost);
 });
#endregion

#region GetAllPosts
app.MapGet("/api/posts", async (IMediator mediator) =>
{
    var getAllPosts = new GetAllPosts();
    var posts = await mediator.Send(getAllPosts);
    return Results.Ok(posts);

});
#endregion

#region UpdatePost
app.MapPut("/api/posts/{id}", async (IMediator mediator, Post post, long id) =>
{
    var updatePost = new UpdatePost() { PostContent = post.Content, PostId = id };
    var updatedPost = await mediator.Send(updatePost);
    return Results.Ok(updatedPost);
});
#endregion

#region DeletePost
app.MapDelete("/api/posts/{id}", async (IMediator mediator, long id) =>
{
    var deletePost = new DeletePost() { PostId = id };
    await mediator.Send(deletePost);
    return Results.NoContent();
});
#endregion

app.Run();
