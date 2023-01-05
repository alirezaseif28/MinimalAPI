using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;
        public PostRepository(SocialDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task DeletePost(long postId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Post>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetPostById(long postId)
        {
            return await _ctx.Post.FirstOrDefaultAsync(x=>x.ID == postId);
        }

        public async Task<Post> InsertPost(Post entity)
        {
            entity.DateCreated = DateTime.Now;
            _ctx.Post.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;

        }

        public Task<Post> UpdatePost(string? content, long postId)
        {
            throw new NotImplementedException();
        }
    }
}
