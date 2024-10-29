using API.Service.Dtos.Category;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace API.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _categoryService.GetAll();
            if (res.StatusCode == 200)
            {
                return View(res.Data);
            }
            return View();
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var res = await _categoryService.GetById(id);
            if (res.StatusCode == 404)
            {
                return NotFound();
            }
            return View(res.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
        {
            var res = await _categoryService.Update(id, dto);
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _categoryService.Remove(id);
            if (res.StatusCode == 404)
            {
                return NotFound();
            }
            return RedirectToAction("index", "category");
        }
    }
}
