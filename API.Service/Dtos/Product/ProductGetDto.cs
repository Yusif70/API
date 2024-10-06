namespace API.Service.Dtos.Product
{
    public class ProductGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Image {  get; set; }
    }
}
