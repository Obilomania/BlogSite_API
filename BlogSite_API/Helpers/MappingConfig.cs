using AutoMapper;
using BlogSite_API.DTOs.CommentDTOs;
using BlogSite_API.DTOs.PostDTOs;
using BlogSite_API.Models;

namespace BlogSite_API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //Post mapping
            CreateMap<PostCreate, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<PostUpdate, Post>().ReverseMap();
            CreateMap<Post, PostGet>().ReverseMap();

            //Comment Mapping
            CreateMap<CommentCreate, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
