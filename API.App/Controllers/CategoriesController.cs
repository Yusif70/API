using API.Service.Dtos.Category;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryPostDto dto)
        {
            var res = await _categoryService.Add(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _categoryService.GetAll();
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _categoryService.GetById(id);
            return StatusCode(res.StatusCode, res.Data);
        }
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    Category category = await _repository.Get(c=>c.Id == id);
        //    return StatusCode(200, category);
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _categoryService.Remove(id);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
        {
            var res = await _categoryService.Update(id, dto);
            return StatusCode(res.StatusCode, res.Message);
        }
    }
}
