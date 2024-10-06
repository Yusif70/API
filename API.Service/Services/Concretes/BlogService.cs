using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Service.ApiResponses;
using API.Service.Dtos.Blog;
using API.Service.Extensions;
using API.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace API.Service.Services.Concretes
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogService(IBlogRepository repository, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> GetAll()
        {
            var blogs = await _repository.GetAll().ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = blogs };
        }

        public async Task<ApiResponse> GetById(Guid guid)
        {
            var blog = await _repository.GetByIdAsync(guid);
            if (blog == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Blog not found" };
            }
            BlogGetDto dto = _mapper.Map<BlogGetDto>(blog);
            return new ApiResponse { StatusCode = 200, Data = dto };
        }
        public async Task<ApiResponse> Add(BlogPostDto dto)
        {
            Blog blog = _mapper.Map<Blog>(dto);
            string path = _env.WebRootPath + "/assets/images/blogs/";
            blog.Image = await dto.File.SaveAsync(path);
            var req = _httpContextAccessor.HttpContext.Request;
            blog.ImageUrl = req.Scheme + "://" + req.Host + "/assets/images/blogs/" + blog.Image;
            blog.CreatedAt = DateTime.Now;
            await _repository.AddAsync(blog);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 201, Message = "Blog created successfully!" };
        }

        public async Task<ApiResponse> Update(Guid guid, BlogPutDto dto)
        {
            Blog updatedBlog = await _repository.GetByIdAsync(guid);
            if (updatedBlog == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Blog not found" };
            }
            string path = _env.WebRootPath + "/assets/images/blogs";
            updatedBlog.Title = dto.Title;
            updatedBlog.Description = dto.Description;
            updatedBlog.CategoryId = dto.CategoryId;
            if (await dto.File.SaveAsync(path) != "")
            {
                string fullPath = _env.WebRootPath + "/assets/images/blogs/" + updatedBlog.Image;
                File.Delete(fullPath);
                updatedBlog.Image = await dto.File.SaveAsync(path);
                var req = _httpContextAccessor.HttpContext.Request;
                updatedBlog.ImageUrl = req.Scheme + "://" + req.Host + "/assets/images/blogs/" + updatedBlog.Image;
            }
            updatedBlog.LastUpdatedAt = DateTime.Now;
            _repository.Update(updatedBlog);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
        public async Task<ApiResponse> Remove(Guid guid)
        {
            Blog blog = await _repository.GetByIdAsync(guid);
            if (blog == null)
            {
                return new ApiResponse { StatusCode = 404, Message = "Blog not found" };
            }
            string fullPath = _env.WebRootPath + "/assets/images/blogs/" + blog.Image;
            File.Delete(fullPath);
            _repository.Remove(blog);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
