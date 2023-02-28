using BlogSite_API.DTOs.CommentDTOs;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite_API.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _context;

        public CommentsController(ICommentRepository context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateComment(CommentCreate commentCreate)
        {
            var id = await _context.CreateCommentAsync(commentCreate);
            return Ok(id);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteComment(CommentDelete commentDelete)
        {
            await _context.DeleteCommentasync(commentDelete);
            return Ok();
        }
    }
}
