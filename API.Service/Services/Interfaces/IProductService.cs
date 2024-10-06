using API.Service.ApiResponses;
using API.Service.Dtos.Product;

namespace API.Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse> Add(ProductPostDto dto);
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(Guid guid);
        Task<ApiResponse> Update(Guid guid, ProductPutDto dto);
        Task<ApiResponse> Remove(Guid guid);
    }
}
