using Microsoft.AspNetCore.Mvc;
using SocialClone.DTO;
using SocialClone.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService){
            _postService = postService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] PostsDTO postDto)
        {
            try
            {
                var post = await _postService.CreatePostAsync(postDto);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userName}")]
        public async Task<IActionResult> GetPostsByUser(string userName)
        {
            var posts = await _postService.GetPostsByUserAsync(userName);
            return Ok(posts);
        }
    }



    

}