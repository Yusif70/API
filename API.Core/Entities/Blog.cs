namespace API.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public string ImageUrl {  get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
