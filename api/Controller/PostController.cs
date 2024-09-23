using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Post;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{

    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPostRepository _postRepo;

        private readonly UserManager<AppUser> _userManager;
        public PostController(ApplicationDBContext context, IPostRepository postRepository, UserManager<AppUser> userManager)
        {
            _context = context;
            _postRepo = postRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postRepo.GetPosts();
            var postDto = posts.Select(s => s.ToPostDto());
            return Ok(postDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var post = _context.Posts.FirstOrDefault(e => e.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post.ToPostDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var postModel = createPostDto.ToPostFromCreateDto();
            postModel.CreatedBy = appUser.UserName;
            postModel.UserId = appUser.Id;
            await _postRepo.CreateAsync(postModel);
            return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, postModel.ToPostDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updateDto)
        {
            var post = await _postRepo.UpdateAsync(id, updateDto);
            if (post == null)
            {
                return BadRequest(new { Message = "Post not found" });
            }
            return Ok(post.ToPostDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _postRepo.Delete(id);
            return NoContent();
        }

      [HttpGet("user/{userId}")]
public async Task<IActionResult> GetMyPosts([FromRoute] string userId)
{
    var appUser = await _userManager.FindByIdAsync(userId);  // Get the user's details

    if (appUser == null)
        return Unauthorized(new { message = "User not found." });

    var posts = await _postRepo.GetPostsByUserId(appUser);  // Fetch posts by the user's ID

    if (posts == null || !posts.Any())
        return NotFound(new { message = "No posts found for this user." });

    var postDtos = posts.Select(p => p.ToPostDto());  // Convert to DTOs if needed
    
    return Ok(postDtos);
}

  

    }
}