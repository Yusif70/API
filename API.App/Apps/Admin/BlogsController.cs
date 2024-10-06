using API.Service.Dtos.Blog;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Apps.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, SuperAdmin")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogPostDto dto)
        {
            var res = await _blogService.Add(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BlogPutDto dto)
        {
            var res = await _blogService.Update(id, dto);
            return StatusCode(res.StatusCode);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _blogService.Remove(id);
            return StatusCode(res.StatusCode);
        }
    }
}
