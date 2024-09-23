using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Add the createdBy field to store the user who created the post
    public string CreatedBy { get; set; } = string.Empty;  
        
        public string? UserId { get; set; }

    public AppUser? User { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
}

}