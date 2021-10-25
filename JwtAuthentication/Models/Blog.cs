using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }

    public class BlogDTO
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
