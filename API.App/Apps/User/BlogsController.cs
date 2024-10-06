using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Apps.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _blogService.GetAll();
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _blogService.GetById(id);
            return StatusCode(res.StatusCode, res.Data);
        }
    }
}
