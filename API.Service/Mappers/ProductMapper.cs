using API.Core.Entities;
using API.Service.Dtos.Product;
using AutoMapper;

namespace API.Service.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductPostDto, Product>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
        }
    }
}
