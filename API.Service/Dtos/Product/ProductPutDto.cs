using Microsoft.AspNetCore.Http;

namespace API.Service.Dtos.Product
{
    public class ProductPutDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile? File { get; set; }
    }
}
