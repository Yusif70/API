using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Apps.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAll();
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _productService.GetById(id);
            return StatusCode(res.StatusCode, res.Data);
        }

    }
}
