using AutoMapper;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository;
using BlogSite_API.Repository.IRepository;
using BlogSite_API.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogSite_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _context;
        private readonly IPhotoService _photoService;
        private readonly IBlobservice _blobService;
        private ApiResponse _response;
        private readonly IMapper _mapper;

        public PostController(IPostRepository context, IPhotoService photoService, IMapper mapper, ILogger<PostController> logger, IBlobservice blobService)
        {
            _context = context;
            _photoService = photoService;
            _mapper = mapper;
            this._response = new();
            _logger = logger;
            _blobService = blobService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAllPost()
        {
            try
            {
                _response.Result = await _context.GetAllPostsAsync();
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetPost(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa Error with Id" + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var post = await _context.GetPostByIdAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
                _response.Result = post;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreatePost([FromForm] PostCreate postCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (postCreate.ImageFile == null || postCreate.ImageFile.Length == 0)
                    {
                        return BadRequest("image field cant be empty");
                    }
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(postCreate.ImageFile.FileName)}";
                    DateTime date = DateTime.Now;
                    Post blogPostCreate = new()
                    {
                        Title = postCreate.Title,
                        Content = postCreate.Content,
                        ImageUrl = await _blobService.UploadBlob(fileName, SD.Storage_Container, postCreate.ImageFile),
                        PostDate = DateTime.Now.ToString("dd-MM-yyyy")
                    };
                    await _context.CreatePostAsync(blogPostCreate);
                    await _context.SaveAsync();
                    _response.Result = blogPostCreate;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(blogPostCreate);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut("{id:int}")]
        //[Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdatePost(int id, [FromForm] PostUpdate postUpdate)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (postUpdate == null || id != postUpdate.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }
                    var postFromDb = await _context.GetPostByIdAsync(id);
                    if (postFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }
                    
                    postFromDb.Title = postUpdate.Title;
                    postFromDb.Content = postUpdate.Content;
                    postFromDb.PostDate = DateTime.Now.ToString("dd-MM-yyyy");

                    if (postUpdate.ImageFile != null && postUpdate.ImageFile.Length > 0)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(postUpdate.ImageFile.FileName)}";
                        await _blobService.DeleteBlob(postFromDb.ImageUrl.Split('/').Last(), SD.Storage_Container);
                        postFromDb.ImageUrl = await _blobService.UploadBlob(fileName, SD.Storage_Container, postUpdate.ImageFile);
                    };
                    await _context.UpdatePostAsync(postFromDb);
                    await _context.SaveAsync();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteAddress(int id)
        {
            try
            {
                if (id== 0)
                {
                    return BadRequest();
                };
                var postFromDb = await _context.GetPostByIdAsync(id);
                if (postFromDb == null)
                {
                    return NotFound();
                }
                await _blobService.DeleteBlob(postFromDb.ImageUrl.Split('/').Last(), SD.Storage_Container);
                int milliseconds = 2000;
                Thread.Sleep(milliseconds);


                await _context.DeletePostAsync(postFromDb);
                await _context.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
