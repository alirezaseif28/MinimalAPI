using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;
        public PostRepository(SocialDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task DeletePost(long postId)
        {
            var post = await _ctx.Post.FirstOrDefaultAsync(x => x.ID == postId);

            if (post == null)
                return;
            _ctx.Post.Remove(post);

            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _ctx.Post.ToListAsync();
        }

        public async Task<Post> GetPostById(long postId)
        {
            return await _ctx.Post.FirstOrDefaultAsync(x => x.ID == postId);
        }

        public async Task<Post> InsertPost(Post entity)
        {
            entity.DateCreated = DateTime.Now;
            _ctx.Post.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<Post> UpdatePost(string? content, long postId)
        {
            var post = await _ctx.Post.FirstOrDefaultAsync(x => x.ID == postId);
            if (post == null)
                throw new Exception("Error");
            post.Content = content;
            post.LastModified = DateTime.Now;
            await _ctx.SaveChangesAsync();
            return post;
        }
    }
}
