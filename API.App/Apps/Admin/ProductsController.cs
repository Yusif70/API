using API.Service.Dtos.Product;
using API.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.App.Apps.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ProductPostDto dto)
        {
            var res = await _productService.Add(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductPutDto dto)
        {
            var res = await _productService.Update(id, dto);
            return StatusCode(res.StatusCode);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _productService.Remove(id);
            return StatusCode(res.StatusCode);
        }
    }
}

