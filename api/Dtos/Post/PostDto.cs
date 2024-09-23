using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Theme { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public List<Comment> Comments {get; set;} = new List<Comment>();

        public string CreatedBy { get; set; } = string.Empty;


    }
}