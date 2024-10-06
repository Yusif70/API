using API.Service.ApiResponses;
using API.Service.Dtos.Auth;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Service.Services.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<ApiResponse> Login([FromForm] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
                if (user == null)
                {
                    return new ApiResponse { StatusCode = 400, Data = "Invalid username or password" };
                }
            }
            bool loggedIn = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (loggedIn)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name, dto.UserNameOrEmail),
                            new(ClaimTypes.NameIdentifier, user.Id)
                        };
                foreach (var role in userRoles)
                {
                    claims.Add(new(ClaimTypes.Role, role));
                }
                var secret_key = _configuration["JWT:secret_key"];
                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));
                var jwtToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:issuer"],
                    audience: _configuration["JWT:audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["JWT:datetime"])),
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return new ApiResponse { StatusCode = 200, Data = token };
            }
            else
            {
                return new ApiResponse { StatusCode = 400, Data = "Invalid username or password" };
            }
        }
        public async Task<ApiResponse> Register([FromForm] RegisterDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.Username);
            if (userExists != null)
            {
                return new ApiResponse { StatusCode = 400, Data = "User with the same username already exists" };
            }
            IdentityUser user = new()
            {
                UserName = dto.Username,
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var res = await _userManager.CreateAsync(user, dto.Password);
            if (!res.Succeeded)
            {
                //return new ApiResponse { Data = res.Errors };
                foreach (var error in res.Errors)
                {
                    //ModelState.AddModelError("", error.Description);
                    return new ApiResponse { Data = error.Description };
                }
                return new ApiResponse { StatusCode = 400 };
            }
            res = await _userManager.AddToRoleAsync(user, "User");
            if (!res.Succeeded)
            {
                //return new ApiResponse { Data = res.Errors };
                foreach (var error in res.Errors)
                {
                    return new ApiResponse { Data = error.Description };
                }
                return new ApiResponse { StatusCode = 400 };
            }
            return new ApiResponse { StatusCode = 201, Data = "User created successfully" };
        }
    }
}
