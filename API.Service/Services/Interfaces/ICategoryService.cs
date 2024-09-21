using API.Service.ApiResponses;
using API.Service.Dtos.Category;

namespace API.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse> Add(CategoryPostDto dto);
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(Guid guid);
        Task<ApiResponse> Update(Guid guid, CategoryPutDto dto);
        Task<ApiResponse> Remove(Guid guid);
    }
}
