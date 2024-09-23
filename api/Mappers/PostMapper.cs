using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Post;
using api.Models;

namespace api.Mappers
{
    public static class PostMapper
    {
       public static PostDto ToPostDto(this Post postModel)
{
    return new PostDto
    {
        Id = postModel.Id,
        Theme = postModel.Theme,
        CreatedAt = postModel.CreatedAt,
        Content = postModel.Content,
        Title = postModel.Title,
   CreatedBy = postModel.CreatedBy,
   
    };
}

public static Post ToPostFromCreateDto(this CreatePostDto createPostDto)
{
    return new Post
    {
        Theme = createPostDto.Theme,
        CreatedAt = createPostDto.CreatedAt,
        Content = createPostDto.Content,
        Title = createPostDto.Title,
        
    
        // CreatedBy = createPostDto.CreatedBy  
    };
}

    }
}