using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Post;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PostRepository : IPostRepository
    {   
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateAsync(Post postModel)
        {
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<Post?> Delete(int id)
        {
           var post = await _context.Posts.Include(s=>s.Comments).FirstOrDefaultAsync(e => e.Id == id);
            if(post == null){
                return null;            
                }
                _context.RemoveRange(post.Comments);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            return post;
        }


        public Task<List<Post>> GetPosts()
        {
           return _context.Posts.ToListAsync();
        }

        public async Task<List<Post?>> GetPostsByUserId(AppUser user)
        {
            return await _context.Posts.Where(p => p.UserId == user.Id).ToListAsync();
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostDto postDto)
        {
             var post = await _context.Posts.FirstOrDefaultAsync(e => e.Id == id);
           if(post == null){
            return null;
           }
           post.Title = postDto.Title;
           post.Content = postDto.Content;
           post.Theme = postDto.Theme;
           post.CreatedAt = postDto.CreatedAt;

           await _context.SaveChangesAsync();
           return post;
        }

       
    }
}

