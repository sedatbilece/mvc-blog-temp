namespace CoreMvcTemplate.Models.Blog
{
    public class DetailBlogViewModel
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? SeoTitle { get; set; }
        public string? Detail { get; set; }
        public string? Content { get; set; }
        public string? HeadImageUrl { get; set; }
        public string?  CreatedAt { get; set; }
        public bool isActive { get; set; } = true;
        public int DisplayOrder { get; set; }
    }
}
