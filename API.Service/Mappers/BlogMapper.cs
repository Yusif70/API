using API.Core.Entities;
using API.Service.Dtos.Blog;
using AutoMapper;

namespace API.Service.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<BlogPostDto, Blog>().ReverseMap();
            CreateMap<Blog, BlogGetDto>().ReverseMap();
        }
    }
}
