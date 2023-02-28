using AutoMapper;
using BlogSite_API.DTOs.CommentDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;

namespace BlogSite_API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public CommentRepository(IGenericRepository<Comment> commentRepository, IMapper mapper, IGenericRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<int> CreateCommentAsync(CommentCreate commentCreate)
        {
            var post = await _postRepository.GetByIdAsync(commentCreate.PostId);
            var entity = _mapper.Map<Comment>(commentCreate);
            entity.Post = post;
            await _commentRepository.InsertAsync(entity);
            await _commentRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteCommentasync(CommentDelete commentDelete)
        {
            var entity = await _commentRepository.GetByIdAsync(commentDelete.Id);
            _commentRepository.Delete(entity);
            await _commentRepository.SaveChangesAsync();
        }
    }
}
