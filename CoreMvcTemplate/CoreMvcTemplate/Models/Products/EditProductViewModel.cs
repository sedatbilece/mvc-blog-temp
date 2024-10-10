namespace CoreMvcTemplate.Models.Products
{
    public class EditProductViewModel
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? SeoTitle { get; set; }
        public string? Detail { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool isActive { get; set; } = true;

        public int DisplayOrder { get; set; }
    }
}
