using API.Core.Entities;
using API.Service.Dtos.Category;
using AutoMapper;

namespace API.Service.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryPostDto, Category>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();
        }
    }
}
