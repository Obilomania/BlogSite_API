using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite_API.Controllers
{
    [Route("api/Post")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postDB;

        public PostsController(IPostRepository postDB)
        {
            _postDB = postDB;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePost([FromBody]PostCreate postCreate)
        {
            var id = await _postDB.CreatePostAsync(postCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePost(PostUpdate postUpdate)
        {
            await _postDB.UpdatePostAsync(postUpdate);
            return Ok();
        }


        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePost(PostDelete postDelete)
        {
            await _postDB.DeletePostAsync(postDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postDB.GetPostAsync(id);
            return Ok(post);
        }

        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postDB.GetAllPostAsync();
            return Ok(posts);
        }
    }
}
