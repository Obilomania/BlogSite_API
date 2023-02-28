using AutoMapper;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using System.Net;

namespace BlogSite_API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Post> _postRepository;

        public PostRepository(IMapper mapper, IGenericRepository<Post> postRepository)
        { 
        
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<int> CreatePostAsync(PostCreate postCreate)
        {
            var entity = _mapper.Map<Post>(postCreate);
            await _postRepository.InsertAsync(entity);
            await _postRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeletePostAsync(PostDelete postDelete)
        {
            var entity = await _postRepository.GetByIdAsync(postDelete.Id);
            _postRepository.Delete(entity);
            await _postRepository.SaveChangesAsync();
        }

        public async Task<PostGet> GetPostAsync(int id)
        {
            var entity = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<PostGet>(entity);
        }

        public async Task<List<PostGet>> GetAllPostAsync()
        {
            var entities = await _postRepository.GetAsync(null, null);
            return _mapper.Map<List<PostGet>>(entities);
        }

        public async Task UpdatePostAsync(PostUpdate postUpdate)
        {
            //var existingEntity = await _addressRepository.GetByIdAsync(addressUpdate.Id);
            var entity = _mapper.Map<Post>(postUpdate);
            _postRepository.Update(entity);
            await _postRepository.SaveChangesAsync();
        }
    }
}
