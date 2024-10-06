using Microsoft.AspNetCore.Http;

namespace API.Service.Dtos.Blog
{
    public class BlogPutDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public Guid CategoryId { get; set; }
    }
}
