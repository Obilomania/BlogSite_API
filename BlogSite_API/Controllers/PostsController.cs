using AutoMapper;
using BlogSite_API.Data;
using BlogSite_API.DTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogSite_API.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PostsController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            IEnumerable<Post> posts = await _db.Posts.ToListAsync();
            return Ok(_mapper.Map<List<PostDTO>>(posts));
        }



        [HttpGet("{id:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var post =_db.Posts
                .Where(p=>p.PostId == id)
                .Include(c=>c.Comments)
                .FirstOrDefault(p => p.PostId ==id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PostDTO>(post));    
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostCreateDTO postCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
             
            if (postCreate == null)
            {
                return BadRequest(postCreate);
            }
            Post model = _mapper.Map<Post>(postCreate);
            _db.Posts.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetPost", new { id = model.PostId }, model);
        }



        [HttpDelete("{id:int}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var post = _db.Posts
                .Where(p => p.PostId == id)
                .Include(c => c.Comments)
                .FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return NoContent();
        }



        [HttpPut("{id:int}", Name = "UpdatePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdateDTO postUpdateDTO)
        {
            if (postUpdateDTO == null || id !=postUpdateDTO.PostId)
            {
                return BadRequest();
            }
            Post model = _mapper.Map<Post>(postUpdateDTO);
            _db.Posts.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
