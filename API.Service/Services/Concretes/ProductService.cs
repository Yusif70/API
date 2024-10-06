using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Service.ApiResponses;
using API.Service.Dtos.Product;
using API.Service.Extensions;
using API.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, IProductRepository repository, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse> GetAll()
        {
            var products = await _repository.GetAll().ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = products };
        }
        public async Task<ApiResponse> GetById(Guid guid)
        {
            Product product = await _repository.GetByIdAsync(guid);
            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Product not found" };
            }
            ProductGetDto dto = _mapper.Map<ProductGetDto>(product);
            return new ApiResponse { StatusCode = 200, Data = dto };
        }
        public async Task<ApiResponse> Add(ProductPostDto dto)
        {
            Product product = _mapper.Map<Product>(dto);
            string path = _env.WebRootPath + "/assets/images/products";
            product.Image = await dto.File.SaveAsync(path);
            var req = _httpContextAccessor.HttpContext.Request;
            product.ImageUrl = req.Scheme + "://" + req.Host + "/assets/images/products/" + product.Image;
            product.CreatedAt = DateTime.Now;
            await _repository.AddAsync(product);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 201, Message = "Product added successfully!" };
        }
        public async Task<ApiResponse> Update(Guid guid, ProductPutDto dto)
        {
            Product updatedProduct = await _repository.GetByIdAsync(guid);
            if (updatedProduct == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Product not found" };
            }
            string path = _env.WebRootPath + "/assets/images/products";
            updatedProduct.Name = dto.Name;
            updatedProduct.Description = dto.Description;
            updatedProduct.Price = dto.Price;
            updatedProduct.CategoryId = dto.CategoryId;
            if (await dto.File.SaveAsync(path) != null)
            {
                string fullPath = _env.WebRootPath + "/assets/images/products/" + updatedProduct.Image;
                File.Delete(fullPath);
                updatedProduct.Image = await dto.File.SaveAsync(path);
                var req = _httpContextAccessor.HttpContext.Request;
                updatedProduct.ImageUrl = req.Scheme + "://" + req.Host + "/assets/images/products/" + updatedProduct.Image;
            }
            updatedProduct.LastUpdatedAt = DateTime.Now;
            _repository.Update(updatedProduct);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
        public async Task<ApiResponse> Remove(Guid guid)
        {
            Product product = await _repository.GetByIdAsync(guid);
            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Product not found" };
            }
            string fullPath = _env.WebRootPath + "/assets/images/products/" + product.Image;
            File.Delete(fullPath);
            _repository.Remove(product);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
