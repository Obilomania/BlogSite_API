using BlogSite_API.Data;
using BlogSite_API.DTOs.CommentDTOs;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _post;

        public CommentController(ApplicationDbContext context, IPostRepository post)
        {
            _context = context;
            _post = post;
        }




        [HttpPost("{postId}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateComment(int postId, CommentCreate comment)
        {
            var post = await _post.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }
            var model = new Comment
            {
                CommentContent = comment.CommentContent,
                CommentedOn = DateTime.UtcNow,
                Post = post,
            };
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }


        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentWithPost(int id)
        {
            var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(p => p.Id == id);
            var model = new PostGet
            {
                //PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                Comments = post.Comments.ToList(),

            };
            return Ok(model);
        }
    }
}
