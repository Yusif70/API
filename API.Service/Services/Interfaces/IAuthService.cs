using API.Service.ApiResponses;
using API.Service.Dtos.Auth;

namespace API.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> Register(RegisterDto dto);
        Task<ApiResponse> Login(LoginDto dto);
    }
}
