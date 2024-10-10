using CoreMvcTemplate.Entities.Data;

namespace CoreMvcTemplate.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string? Detail { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
    }
}
