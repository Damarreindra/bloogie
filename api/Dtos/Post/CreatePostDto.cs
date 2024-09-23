using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Post
{
    public class CreatePostDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Title must be 5 character or more")]
        [MaxLength(280, ErrorMessage ="Title cant be more than 280")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Theme { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage ="Title must be 5 character or more")]
        [MaxLength(280, ErrorMessage ="Title cant be more than 280")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        //  public string CreatedBy { get; set; } = string.Empty;
    
    }
}