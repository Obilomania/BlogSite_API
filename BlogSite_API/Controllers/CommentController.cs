using BlogSite_API.Data;
using BlogSite_API.DTOs.CommentDTOs;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _post;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApiResponse _response;


        public CommentController(ApplicationDbContext context, IPostRepository post, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _post = post;
            _userManager = userManager;
            this._response = new();

        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateComment(CommentCreate comment)
        {
            try
            {
                var model = new Comment
                {
                    CommentContent = comment.CommentContent,
                    CommentedOn = DateTime.Now.ToString("dd-MM-yyyy"),
                    UserId = comment.UserId,
                    Commenter = comment.Commenter,
                    PostId = comment.PostId,
                };
                await _context.AddAsync(model);
                _context.SaveChanges();
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentWithPost(int id)
        {
            var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(p => p.Id == id);
            var model = new PostGet
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                Comments = post.Comments.ToList(),

            };
            return Ok(model);
        }
    }
}
