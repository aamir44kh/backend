using JwtAuthentication.Helper;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace JwtAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ActiveUser _activeUser;
        public BlogController(ApplicationDbContext context,ActiveUser  activeUser)
        {
            _context = context;
            _activeUser = activeUser;
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return new JsonResult(_context.Blog.ToList());
        }
        [HttpPost]
        public IActionResult CreatePost([FromForm]BlogDTO  model)
        {
            Blog blog = new Blog();
            blog.Title = model.Title;
            blog.WrittenBy = _activeUser.UserName;
            blog.WrittenDate = DateTime.Now;
            blog.Content = model.Content;
            if(model.Image !=null)
            {
                var basePath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Blogs");
                if(!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(basePath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                blog.ImageUrl = Path.Combine("Blogs", uniqueFileName);
            }
            _context.Blog.Add(blog);
            _context.SaveChanges();
            return Ok();
        }
    }
}
