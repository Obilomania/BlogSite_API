using AutoMapper;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _context;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public PostController(IPostRepository context, IPhotoService photoService, IMapper mapper)
        {
            _context = context;
            _photoService = photoService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _context.GetAllPostsAsync();
            return Ok(posts);
        }


        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPost(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var post = await _context.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePost([FromForm] PostCreate postCreate)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(postCreate.ImageFile);
                if (postCreate.ImageFile == null || postCreate.ImageFile.Length == 0)
                {
                    return BadRequest("image field cant be empty");
                }
                Post blogPostCreate = new()
                {
                    Title = postCreate.Title,
                    Content = postCreate.Content,
                    ImageUrl = result.Url.ToString(),
                    PostDate = DateTime.UtcNow
                };
                await _context.CreatePostAsync(blogPostCreate);
                await _context.SaveAsync();
                return Ok(blogPostCreate);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePost(int id, [FromForm] PostUpdate postUpdate)
        {
            try
            {

                var postFromDb = await _context.GetPostByIdAsync(id);
                if (postUpdate.ImageFile == null || postUpdate.ImageFile.Length == 0)
                {
                    return BadRequest("image field cant be empty");
                }
                if (postUpdate.ImageFile != null && postUpdate.ImageFile.Length > 0)
                {
                    await _photoService.DeletePhotoAsync(postFromDb.ImageUrl);
                    var result = await _photoService.AddPhotoAsync(postUpdate.ImageFile);
                };
                postFromDb.Title = postUpdate.Title;
                postFromDb.Content = postUpdate.Content;
                postFromDb.PostDate = postUpdate.UpdatedTime;
                await _context.UpdatePostAsync(postFromDb);
                await _context.SaveAsync();
                return Ok(postFromDb);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddress(PostDelete postDelete)
        {
            if (postDelete.Id == 0)
            {
                return BadRequest();
            };
            var postFromDb = await _context.GetPostByIdAsync(postDelete.Id);
            if (postFromDb == null)
            {
                return NotFound();
            }
            await _photoService.DeletePhotoAsync(postFromDb.ImageUrl);
            await _context.DeletePostAsync(postFromDb);
            await _context.SaveAsync();
            return Ok();
        }
    }
}
