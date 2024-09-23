using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Post;
using api.Models;

namespace api.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post?>> GetPosts();

        Task<List<Post?>> GetPostsByUserId(AppUser user);

        Task<Post> CreateAsync(Post postModel);

        Task<Post?> UpdateAsync(int id, UpdatePostDto postDto);

        Task<Post?> Delete(int id);
    }
}