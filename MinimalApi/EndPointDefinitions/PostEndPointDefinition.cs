using MinimalApi.Abstractions;
using MediatR;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MinimalApi.Filters;

namespace MinimalApi.EndPointDefinitions
{
    public class PostEndPointDefinition : IEndPointDefinition
    {
        public void RegisterEndPoint(WebApplication app)
        {
            var posts = app.MapGroup("/api/posts");
            #region GetPostById
            posts.MapGet("/{id}", GetPostById).WithName("GetPostById");
            #endregion

            #region CreatePost
            posts.MapPost("/", CreatePost).AddEndpointFilter<PostValidationFilter>();
            #endregion

            #region GetAllPosts
            posts.MapGet("/", GetAllPosts);
            #endregion

            #region UpdatePost
            posts.MapPut("/{id}", UpdatePost).AddEndpointFilter<PostValidationFilter>();
            #endregion

            #region DeletePost
            posts.MapDelete("/{id}", DeletePost);
            #endregion

        }

        private async Task<IResult> GetPostById(IMediator mediator, long id)
        {
            var getPost = new GetPostById() { PostId = id };
            var post = await mediator.Send(getPost);
            return Results.Ok(post);
        }
        private async Task<IResult> CreatePost(IMediator mediator, Post post)
        {
            var createPost = new CreatePost() { PostContent = post.Content };
            var createdPost = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostById", new { createdPost.ID }, createdPost);
        }

        private async Task<IResult> GetAllPosts(IMediator mediator)
        {
            var getAllPosts = new GetAllPosts();
            var posts = await mediator.Send(getAllPosts);
            return Results.Ok(posts);

        }
        private async Task<IResult> UpdatePost(IMediator mediator, Post post, long id)
        {
            var updatePost = new UpdatePost() { PostContent = post.Content, PostId = id };
            var updatedPost = await mediator.Send(updatePost);
            return Results.Ok(updatedPost);
        }
        private async Task<IResult> DeletePost(IMediator mediator, long id)
        {
            var deletePost = new DeletePost() { PostId = id };
            await mediator.Send(deletePost);
            return Results.NoContent();
        }
    }
}
