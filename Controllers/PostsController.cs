using BAITZ_BLOG_API.Domain;
using BAITZ_BLOG_API.Interfaces;
using BAITZ_BLOG_API.Services;
using Microsoft.AspNetCore.Mvc;
using BAITZ_BLOG_API.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace BAITZ_BLOG_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllPost()
        {
            var p = _postService.GetAll();
            return Ok(p);
        }
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPostById(int id)
        {
            var p = _postService.GetById(id);
            var databytes = System.IO.File.ReadAllBytes(p.ImageUrl);
            return File(databytes, "image/png");
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostCreate(PostCreateModel postcreatemodel) 
        {
            _postService.Create(postcreatemodel);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdatePost(int id, PostCreateModel postcreatemodel) 
        {
            _postService.Update(id, postcreatemodel);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _postService.Delete(id);
            return Ok();
        }



    }
}

