using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Service.ApiResponses;
using API.Service.Dtos.Category;
using API.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ApiResponse> GetAll()
        {
            List<Category> categories = await _repository.GetAll().ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = categories };
        }

        public async Task<ApiResponse> GetById(Guid guid)
        {
            Category category = await _repository.GetByIdAsync(guid);
            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Category not found" };
            }
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return new ApiResponse { StatusCode = 200, Data = dto };
        }
        public async Task<ApiResponse> Add(CategoryPostDto dto)
        {
            Category category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.Now;
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 200, Data = category, Message = "Category created successfully!" };
        }

        public async Task<ApiResponse> Update(Guid guid, CategoryPutDto dto)
        {
            Category updatedCategory = await _repository.GetByIdAsync(guid);
            if (updatedCategory == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Category not found" };
            }
            updatedCategory.Name = dto.Name;
            updatedCategory.LastUpdatedAt = DateTime.Now;
            _repository.Update(updatedCategory);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Message = "Category updated successfully!" };
        }
        public async Task<ApiResponse> Remove(Guid guid)
        {
            Category category = await _repository.GetByIdAsync(guid);
            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Category not found" };
            }
            _repository.Remove(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Message = "Category deleted successfully!" };
        }
    }
}
