using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPosts();
        Task<Post> GetPostById(long postId);
        Task<Post> InsertPost(Post entity);
        Task<Post> UpdatePost(string? content, long postId);
        Task DeletePost(long postId);

    }
}
