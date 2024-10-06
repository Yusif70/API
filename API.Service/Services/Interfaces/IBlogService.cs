using API.Service.ApiResponses;
using API.Service.Dtos.Blog;

namespace API.Service.Services.Interfaces
{
    public interface IBlogService
    {
        Task<ApiResponse> Add(BlogPostDto dto);
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(Guid guid);
        Task<ApiResponse> Update(Guid guid, BlogPutDto dto);
        Task<ApiResponse> Remove(Guid guid);
    }
}
