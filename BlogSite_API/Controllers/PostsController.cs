using BlogSite_API.DTOs;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite_API.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _db;

        public PostsController(IPostRepository db)
        {
            _db = db;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
            var posts = _db.GetAllPosts();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(posts);
        }



        [HttpGet("{id:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PostDTO> GetPost(int id)
        {
            var post = _db.GetById(id);
            if (post == null) { return NotFound(); };
            return Ok(post);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostDTO> CreatePost([FromBody] PostDTO post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
             
            if (post == null)
            {
                return BadRequest(post);
            }
            if(post.PostId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtRoute("GetPost", new { id = post.PostId }, post);
        }
    }
}
